using ISOtrainSCIMIntegration.ApiWeb.Services;
using Microsoft.AspNetCore.Mvc.Formatters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Opción A: Scoped (Recomendado para la mayoría de las APIs y DBContext)
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

// 1. Agregar servicios de Swagger
builder.Services.AddControllers(
    options =>
    {
        // Buscamos el formateador de System.Text.Json
        var jsonInputFormatter = options.InputFormatters
            .OfType<SystemTextJsonInputFormatter>()
            .FirstOrDefault();

        if (jsonInputFormatter != null)
        {
            jsonInputFormatter.SupportedMediaTypes.Add("text/plain");
            // Esto es lo que DummyIDP está enviando y .NET rechaza
            jsonInputFormatter.SupportedMediaTypes.Add("application/scim+json");
        }
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(); // Acceso en /swagger
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
