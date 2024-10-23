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
app.MapGet("/saludo", () => "Bienvenidos .NET CORE");
app.MapPost("/saludo2", () => "Curso minimal API .NET CORE");
app.UseHttpsRedirection();
app.Run();
