using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using PriorityTask.API;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

// === 1. CONFIGURACIÓN DE CORS ===
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Conexión a la base de datos PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("Postgres")
    ?? throw new InvalidOperationException("Connection string 'Postgres' not found.");

builder.Services.AddDbContext<PriorityTaskAPIContext>(options =>
    options.UseNpgsql(connectionString));

// Controladores con manejo de ciclos
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Pipeline de Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// === 2. ACTIVAR EL MIDDLEWARE DE CORS (debe ir antes de UseAuthorization) ===
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();