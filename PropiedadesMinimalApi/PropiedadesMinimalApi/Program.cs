using Microsoft.AspNetCore.Http; // Para usar Results.Ok
using Microsoft.AspNetCore.Builder; // Para usar WebApplication, MapGet, MapPost
using Microsoft.AspNetCore.Mvc; // Para usar [FromBody]

using PropiedadesMinimalApi.Datos;
using PropiedadesMinimalApi.Modelos;
using PropiedadesMinimalApi.Modelos.DTOS;
using PropiedadesMinimalApi.Mapas;
using AutoMapper;
using FluentValidation;
using System.Net;
using Microsoft.Extensions.Logging;


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

    RespuestaAPI respuesta = new RespuestaAPI();

    logger.Log(LogLevel.Information, "Carga todas las propiedades");

    respuesta.Resultado = DatosPropiedad.listaPropiedades;
    respuesta.Success = true;
    respuesta.codigoEstado = HttpStatusCode.OK;
    return Results.Ok(respuesta);


}).WithName("ObtenerPropiedades").Produces<IEnumerable<RespuestaAPI>>(200); ;


app.MapGet("/api/propiedades/{id:int}", (int id) =>
{
    RespuestaAPI respuesta = new RespuestaAPI();

    respuesta.Resultado = DatosPropiedad.listaPropiedades.FirstOrDefault(p => p.IdPropiedad == id);
    respuesta.Success = true;
    respuesta.codigoEstado = HttpStatusCode.OK;
    return Results.Ok(respuesta);

}).WithName("ObtenerPropiedad").Produces<RespuestaAPI>(200);

// Crear propiedad
app.MapPost("/api/propiedades", async (IMapper _mapper, 
    IValidator<CrearPropiedadDTO> _validacion, [FromBody] CrearPropiedadDTO crearPropiedadDTO) =>
{
    RespuestaAPI respuesta = new RespuestaAPI() { Success = false, codigoEstado = HttpStatusCode.BadRequest};

    var resultadoValidaciones = await _validacion.ValidateAsync(crearPropiedadDTO);

    //Si el resultado de las validaciones no es valido, se retorna un BadRequest con el primer error
    if (!resultadoValidaciones.IsValid)
    {
        respuesta.Errores.Add(resultadoValidaciones.Errors.FirstOrDefault().ToString());
        return Results.BadRequest(respuesta);
    }


    if (DatosPropiedad.listaPropiedades.FirstOrDefault(p => p.Nombre.ToLower() == crearPropiedadDTO.Nombre.ToLower()) != null)
    {
        respuesta.Errores.Add("Ya existe una propiedad con ese nombre");
        return Results.BadRequest(respuesta);
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

    //return Results.CreatedAtRoute("ObtenerPropiedad", new {id = propiedad.IdPropiedad}, propiedadDTO);

    respuesta.Resultado = propiedadDTO;
    respuesta.Success = true;
    respuesta.codigoEstado = HttpStatusCode.Created;
    return Results.Ok(respuesta);

}).WithName("CrearPropiedad").Accepts<CrearPropiedadDTO>("application/json").Produces<RespuestaAPI>(201).Produces(400);

//Actualiza Propiedad
app.MapPut("/api/propiedades", async (IMapper _mapper,
    IValidator<ActualizarPropiedadDTO> _validacion, [FromBody] ActualizarPropiedadDTO actualizarPropiedadDTO) =>
{
    RespuestaAPI respuesta = new RespuestaAPI() { Success = false, codigoEstado = HttpStatusCode.BadRequest };

    var resultadoValidaciones = await _validacion.ValidateAsync(actualizarPropiedadDTO);

    if (!resultadoValidaciones.IsValid)
    {
        respuesta.Errores.Add(resultadoValidaciones.Errors.FirstOrDefault().ToString());
        return Results.BadRequest(respuesta);
    }

    //if (DatosPropiedad.listaPropiedades.FirstOrDefault(p => p.Nombre.ToLower() == crearPropiedadDTO.Nombre.ToLower()) != null)
    //{
    //    respuesta.Errores.Add("Ya existe una propiedad con ese nombre");
    //    return Results.BadRequest(respuesta);
    //}

    Propiedad propiedadDesdeBD = DatosPropiedad.listaPropiedades.FirstOrDefault(p => p.IdPropiedad == actualizarPropiedadDTO.IdPropiedad);

    propiedadDesdeBD.Nombre = actualizarPropiedadDTO.Nombre;
    propiedadDesdeBD.Descripcion = actualizarPropiedadDTO.Descripcion;
    propiedadDesdeBD.Ubicacion = actualizarPropiedadDTO.Ubicacion;
    propiedadDesdeBD.Activa = actualizarPropiedadDTO.Activa;

    respuesta.Resultado = _mapper.Map<PropiedadDTO>(propiedadDesdeBD); ;
    respuesta.Success = true;
    respuesta.codigoEstado = HttpStatusCode.Created;
    return Results.Ok(respuesta);

}).WithName("ActualizarPropiedad").Accepts<ActualizarPropiedadDTO>("application/json").Produces<RespuestaAPI>(200).Produces(400);


app.UseHttpsRedirection();
app.Run();
