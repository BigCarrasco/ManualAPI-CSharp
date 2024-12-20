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
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

//Configuracion de la base de datos
builder.Services.AddDbContext<ApplicationDbContext>(opciones => opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSql")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//dependencia AutoMapper
builder.Services.AddAutoMapper(typeof(ConfiguracionMapas));

//a�adir validaciones
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Obtener ALL
app.MapGet("/api/propiedades", async (ApplicationDbContext _bd, ILogger<Program> logger) => 
{

    RespuestaAPI respuesta = new RespuestaAPI();

    logger.Log(LogLevel.Information, "Carga todas las propiedades");

    //respuesta.Resultado = DatosPropiedad.listaPropiedades;
    respuesta.Resultado = _bd.Propiedad;
    respuesta.Success = true;
    respuesta.codigoEstado = HttpStatusCode.OK;
    return Results.Ok(respuesta);


}).WithName("ObtenerPropiedades").Produces<IEnumerable<RespuestaAPI>>(200); ;

//Obtener por ID
app.MapGet("/api/propiedades/{id:int}", async (ApplicationDbContext _bd, int id) =>
{
    RespuestaAPI respuesta = new RespuestaAPI();

    //respuesta.Resultado = DatosPropiedad.listaPropiedades.FirstOrDefault(p => p.IdPropiedad == id);
    respuesta.Resultado = await _bd.Propiedad.FirstOrDefaultAsync(p => p.IdPropiedad == id);
    respuesta.Success = true;
    respuesta.codigoEstado = HttpStatusCode.OK;
    return Results.Ok(respuesta);

}).WithName("ObtenerPropiedad").Produces<RespuestaAPI>(200);

// Crear propiedad
app.MapPost("/api/propiedades", async (ApplicationDbContext _bd, IMapper _mapper, 
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


    if (await _bd.Propiedad.FirstOrDefaultAsync(p => p.Nombre.ToLower() == crearPropiedadDTO.Nombre.ToLower()) != null)
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

    /*Codigo que ya genra nuestra base de datos:
    //propiedad.IdPropiedad = DatosPropiedad.listaPropiedades.OrderByDescending(p => p.IdPropiedad).FirstOrDefault().IdPropiedad + 1;
    */

    //PropiedadDTO propiedadDTO = new PropiedadDTO()
    //{
    //    IdPropiedad = propiedad.IdPropiedad,
    //    Nombre = propiedad.Nombre,
    //    Descripcion = propiedad.Descripcion,
    //    Ubicacion = propiedad.Ubicacion,
    //    Activa = propiedad.Activa
    //};
    //DatosPropiedad.listaPropiedades.Add(propiedad);
    await _bd.Propiedad.AddAsync(propiedad);
    await _bd.SaveChangesAsync();

    PropiedadDTO propiedadDTO = _mapper.Map<PropiedadDTO>(propiedad);

    //return Results.CreatedAtRoute("ObtenerPropiedad", new {id = propiedad.IdPropiedad}, propiedadDTO);

    respuesta.Resultado = propiedadDTO;
    respuesta.Success = true;
    respuesta.codigoEstado = HttpStatusCode.Created;
    return Results.Ok(respuesta);

}).WithName("CrearPropiedad").Accepts<CrearPropiedadDTO>("application/json").Produces<RespuestaAPI>(201).Produces(400);

//Actualiza Propiedad
app.MapPut("/api/propiedades", async (ApplicationDbContext _bd, IMapper _mapper,
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

    Propiedad propiedadDesdeBD = await _bd.Propiedad.FirstOrDefaultAsync(p => p.IdPropiedad == actualizarPropiedadDTO.IdPropiedad);

    propiedadDesdeBD.Nombre = actualizarPropiedadDTO.Nombre;
    propiedadDesdeBD.Descripcion = actualizarPropiedadDTO.Descripcion;
    propiedadDesdeBD.Ubicacion = actualizarPropiedadDTO.Ubicacion;
    propiedadDesdeBD.Activa = actualizarPropiedadDTO.Activa;

    await _bd.SaveChangesAsync();

    respuesta.Resultado = _mapper.Map<PropiedadDTO>(propiedadDesdeBD); ;
    respuesta.Success = true;
    respuesta.codigoEstado = HttpStatusCode.Created;
    return Results.Ok(respuesta);

}).WithName("ActualizarPropiedad").Accepts<ActualizarPropiedadDTO>("application/json").Produces<RespuestaAPI>(200).Produces(400);

//Borrar por ID
app.MapDelete("/api/propiedades/{id:int}", async (ApplicationDbContext _bd, int id) =>
{
    RespuestaAPI respuesta = new RespuestaAPI() { Success = false, codigoEstado = HttpStatusCode.BadRequest };

    //obtener el id de la propiedad a eliminar
    Propiedad propiedadDesdeBD = await _bd.Propiedad.FirstOrDefaultAsync(p => p.IdPropiedad == id);

    if (propiedadDesdeBD != null)
    {
        _bd.Propiedad.Remove(propiedadDesdeBD);
        await _bd.SaveChangesAsync();
        respuesta.Success = true;
        respuesta.codigoEstado = HttpStatusCode.NoContent;
        return Results.Ok(respuesta);
    }
    else
    {
        respuesta.Errores.Add("No se encontro la propiedad a eliminar");
        return Results.BadRequest(respuesta);
    }

});


app.UseHttpsRedirection();
app.Run();
