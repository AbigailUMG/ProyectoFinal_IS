using Microsoft.EntityFrameworkCore;
using BackendApi.Models;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LaSurtidoraBuenPrecioIsContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL")));

builder.Services.AddControllers().AddJsonOptions(opt =>
{ 
   opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});



// Definición de un nombre para la política CORS
var misReglasCors = "ReglasCors";

// Agregando CORS al servicio de la aplicación
builder.Services.AddCors(opt =>
{
    // Agregando una política CORS con el nombre definido anteriormente
    opt.AddPolicy(name: misReglasCors, builder =>
    {
        // Permitiendo cualquier origen (dominio)
        builder.AllowAnyOrigin()

               // Permitiendo cualquier encabezado
               .AllowAnyHeader()

               // Permitiendo cualquier método HTTP (GET, POST, etc.)
               .AllowAnyMethod();
    });
});
{


    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    // Aqui lo activamos
    app.UseCors(misReglasCors);

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
