using Microsoft.EntityFrameworkCore;
using BackendApi.Models;
using System.Text.Json.Serialization;


// Importamos las librerias
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// CONFIGURAMOS NUESTRO TOKEN
builder.Configuration.AddJsonFile("appsettings.json");
var secretkey = builder.Configuration.GetSection("settings").GetSection("secretkey").ToString();
var keyBytes = Encoding.UTF8.GetBytes(secretkey);


// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(options =>
//     {
//         options.RequireHttpsMetadata = false;
//         options.SaveToken = true;
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuerSigningKey = true,
//             IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
//             ValidateIssuer = false,
//             ValidateAudience = false
//         };
//         options.Events = new JwtBearerEvents
//         {
//             OnTokenValidated = context =>
//             {
//                 var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
//                 if (claimsIdentity != null && claimsIdentity.IsAuthenticated)
//                 {
//                     var roleClaim = claimsIdentity.FindFirst(ClaimTypes.Role);
//                     if (roleClaim == null || roleClaim.Value != "administrador" && roleClaim.Value != "vendedor")
//                     {
//                         context.Fail("Unauthorized");
//                     }
//                 }
//                 return Task.CompletedTask;
//             }
//         };
//     });

// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(options =>
//     {
//         options.RequireHttpsMetadata = false;
//         options.SaveToken = true;
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuerSigningKey = true,
//             IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
//             ValidateIssuer = false,
//             ValidateAudience = false
//         };
//         options.Events = new JwtBearerEvents
//         {
//             OnTokenValidated = context =>
//             {
//                 var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
//                 if (claimsIdentity != null && claimsIdentity.IsAuthenticated)
//                 {
//                     var roleClaim = claimsIdentity.FindFirst(ClaimTypes.Role);
//                     if (roleClaim == null || !IsValidRole(roleClaim.Value))
//                     {
//                         context.Fail("Unauthorized");
//                     }
//                 }
//                 return Task.CompletedTask;
//             }
//         };
//     });

// bool IsValidRole(string role)
// {
//     return role == "administrador" || role == "vendedor";
// }

// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(options =>
//     {
//         options.RequireHttpsMetadata = false;
//         options.SaveToken = true;
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuerSigningKey = true,
//             IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
//             ValidateIssuer = false,
//             ValidateAudience = false
//         };
//         options.Events = new JwtBearerEvents
//         {
//             OnTokenValidated = context =>
//             {
//                 var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
//                 if (claimsIdentity != null && claimsIdentity.IsAuthenticated)
//                 {
//                     var roleClaim = claimsIdentity.FindFirst(ClaimTypes.Role);
//                     if (roleClaim == null || roleClaim.Value != "administrador" && roleClaim.Value != "vendedor")
//                     {
//                         context.Fail("Unauthorized");
//                     }
//                 }
//                 return Task.CompletedTask;
//             }
//         };
//     });
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
            ValidateIssuer = false,
            ValidateAudience = false
        };
        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = context =>
            {
                var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
                if (claimsIdentity != null && claimsIdentity.IsAuthenticated)
                {
                    var roleClaim = claimsIdentity.FindFirst(ClaimTypes.Role);
                    if (roleClaim == null || !int.TryParse(roleClaim.Value, out int roleId) || (roleId != 5 && roleId != 2))
                    {
                        context.Fail("Unauthorized");
                    }
                }
                return Task.CompletedTask;
            }
        };  
    });
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


    //ESTO ES PARTE DEL TOKEN
    app.UseAuthentication();    

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
