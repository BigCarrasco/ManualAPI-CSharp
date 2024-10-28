using Microsoft.AspNetCore.Http; // Para usar Results.Ok
using Microsoft.AspNetCore.Builder; // Para usar WebApplication, MapGet, MapPost
using Microsoft.AspNetCore.Mvc; // Para usar [FromBody]

using PropiedadesMinimalApi.Datos;
using PropiedadesMinimalApi.Modelos;
using PropiedadesMinimalApi.Modelos.DTOS;
using PropiedadesMinimalApi.Mapas;
using AutoMapper;
using FluentValidation;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//dependencia AutoMapper
builder.Services.AddAutoMapper(typeof(ConfiguracionMapas));

//añadir validaciones
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapGet("/api/propiedades", (ILogger<Program> logger) => 
{
    logger.Log(LogLevel.Information, "Carga todas las propiedades");
    return Results.Ok(DatosPropiedad.listaPropiedades);
}).WithName("ObtenerPropiedades").Produces<IEnumerable<Propiedad>>(200); ;


app.MapGet("/api/propiedades/{id:int}", (int id) =>
{
    return Results.Ok(DatosPropiedad.listaPropiedades.FirstOrDefault(p => p.IdPropiedad == id));
}).WithName("ObtenerPropiedad").Produces<Propiedad>(200);

// Crear propiedad
app.MapPost("/api/propiedades", (IMapper _mapper, 
    IValidator<CrearPropiedadDTO> _validacion, [FromBody] CrearPropiedadDTO crearPropiedadDTO) =>
{

    var resultadoValidaciones = _validacion.ValidateAsync(crearPropiedadDTO).GetAwaiter().GetResult();

    //Si el resultado de las validaciones no es valido, se retorna un BadRequest con el primer error
    if (!resultadoValidaciones.IsValid)
    {
        return Results.BadRequest(resultadoValidaciones.Errors.FirstOrDefault().ToString());
    }


    if (DatosPropiedad.listaPropiedades.FirstOrDefault(p => p.Nombre.ToLower() == crearPropiedadDTO.Nombre.ToLower()) != null)
    {
        return Results.BadRequest("Ya existe una propiedad con ese nombre");
    }

    //Propiedad propiedad = new Propiedad()
    //{
    //    Nombre = crearPropiedadDTO.Nombre,
    //    Descripcion = crearPropiedadDTO.Descripcion,
    //    Ubicacion = crearPropiedadDTO.Ubicacion,
    //    Activa = crearPropiedadDTO.Activa
    //};

    Propiedad propiedad = _mapper.Map<Propiedad>(crearPropiedadDTO);

    propiedad.IdPropiedad = DatosPropiedad.listaPropiedades.OrderByDescending(p => p.IdPropiedad).FirstOrDefault().IdPropiedad + 1;

    //PropiedadDTO propiedadDTO = new PropiedadDTO()
    //{
    //    IdPropiedad = propiedad.IdPropiedad,
    //    Nombre = propiedad.Nombre,
    //    Descripcion = propiedad.Descripcion,
    //    Ubicacion = propiedad.Ubicacion,
    //    Activa = propiedad.Activa
    //};

    PropiedadDTO propiedadDTO = _mapper.Map<PropiedadDTO>(propiedad);

    DatosPropiedad.listaPropiedades.Add(propiedad);

    return Results.CreatedAtRoute("ObtenerPropiedad", new {id = propiedad.IdPropiedad}, propiedadDTO);

}).WithName("CrearPropiedad").Accepts<CrearPropiedadDTO>("application/json").Produces<PropiedadDTO>(201).Produces(400);

app.UseHttpsRedirection();
app.Run();
