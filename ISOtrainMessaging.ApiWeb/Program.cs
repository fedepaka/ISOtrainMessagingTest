using ISOtrainMessaging.BussinesLayer.Implementation;
using ISOtrainMessaging.BussinesLayer.Interface;
using ISOtrainMessaging.Model;
using ISOtrainMessaging.TemplateService.Implementation;
using ISOtrainMessaging.TemplateService.Interface;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<ISendingStrategy, SmtpMessageStrategy>();
builder.Services.AddScoped<ITemplateService, TemplateService>();
builder.Services.AddScoped<ISenderBL, SenderBL>();

// Mapeo de la sección del appsettings a la clase
builder.Services.Configure<NotificationSettings>(
    builder.Configuration.GetSection("NotificationSettings"));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Esto convierte los Enums a Strings globalmente
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Agrega el generador de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    //Enable the middleware to serve the Swagger JSON document
    app.UseSwagger();
    //Enable the middleware to serve the Swagger user interface
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
