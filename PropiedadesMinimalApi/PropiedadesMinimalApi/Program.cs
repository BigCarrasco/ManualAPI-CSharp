using Microsoft.AspNetCore.Http; // Para usar Results.Ok
using Microsoft.AspNetCore.Builder; // Para usar WebApplication, MapGet, MapPost
using Microsoft.AspNetCore.Mvc; // Para usar [FromBody]

using PropiedadesMinimalApi.Datos;
using PropiedadesMinimalApi.Modelos;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Primeros endpoints (Los endpoints deben estar antes de app.Run() )
//app.MapGet("/saludo/{id:int}", (int id) =>
//{
//    //return Results.BadRequest();
//    return Results.Ok("El ID es:" + id);
//});

//app.MapPost("/saludo2", () => "Curso minimal API .NET CORE");


/*Obtener todas las propiedades*/
app.MapGet("/api/propiedades", (ILogger<Program> logger) => //Inyeccion de dependencias //Ilogger viene por defecto
{
    logger.Log(LogLevel.Information, "Carga todas las propiedades");
    return Results.Ok(DatosPropiedad.listaPropiedades);
}).WithName("ObtenerPropiedades").Produces<IEnumerable<Propiedad>>(200); ;

/*Obtener propiedad individual */
app.MapGet("/api/propiedades/{id:int}", (int id) =>
{
    return Results.Ok(DatosPropiedad.listaPropiedades.FirstOrDefault(p => p.IdPropiedad == id));
}).WithName("ObtenerPropiedad").Produces<Propiedad>(200);

//Crear Propiedad
app.MapPost("/api/propiedades", ([FromBody] Propiedad propiedad) =>
{
    //Validar id de la propiedad y que el nombre no esté vacio
    if (propiedad.IdPropiedad != 0 || string.IsNullOrEmpty(propiedad.Nombre))
    {
        // Si el ID no es 0 o el nombre está vacío, retorna un error de solicitud incorrecta
        return Results.BadRequest("La propiedad no puede tener un ID incorrecto o el nombre no puede estar vacio");
    }

    // Verificar si ya existe una propiedad con el mismo nombre (ignorando mayúsculas/minúsculas)
    if (DatosPropiedad.listaPropiedades.FirstOrDefault(p => p.Nombre.ToLower() == propiedad.Nombre.ToLower()) != null)
    {
        // Si ya existe una propiedad con el mismo nombre, retorna un error de solicitud incorrecta
        return Results.BadRequest("Ya existe una propiedad con ese nombre");
    }

    // Asignar un nuevo ID a la propiedad. Se obtiene el ID más alto de la lista y se incrementa en 1
    propiedad.IdPropiedad = DatosPropiedad.listaPropiedades.OrderByDescending(p => p.IdPropiedad).FirstOrDefault().IdPropiedad + 1;

    // Agregar la nueva propiedad a la lista de propiedades
    DatosPropiedad.listaPropiedades.Add(propiedad);

    /* Retornar la lista actualizada de propiedades como respuesta */
    //return Results.Ok(DatosPropiedad.listaPropiedades);
    /*Genera un 201 como respuesta, pero no coloca la location adecuada*/
    //return Results.Created("/api/propiedades/{propiedad.IdPropiedad}", propiedad);
    return Results.CreatedAtRoute("ObtenerPropiedad", new {id = propiedad.IdPropiedad}, propiedad);

}).WithName("CrearPropiedad").Accepts<Propiedad>("application/json").Produces<Propiedad>(201).Produces(400);

app.UseHttpsRedirection();
app.Run();
