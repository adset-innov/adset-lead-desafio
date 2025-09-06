using AdSet.Lead.Infrastructure.IoC;
using AdSet.Lead.Infrastructure.Settings;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Default Setup
builder.Services.AddControllers();

// Db Context
builder.Services.AddAppDbContext(builder.Configuration.GetConnectionString("DefaultConnection"));

// Logging
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .Enrich.WithEnvironmentName()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

// Repositories
builder.Services.AddRepositories();

// Use Cases
builder.Services.AddUseCases();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.Configure<OpenApiSettings>(
    builder.Configuration.GetSection("OpenApiSettings"));
builder.Services.AddOpenApi("v1");

// Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("DefaultCorsPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var app = builder.Build();

// Middlewares
app.UseSerilogRequestLogging();
app.UseCors("DefaultCorsPolicy");

// Run
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Title = "AdSet Lead API";
        options.Theme = ScalarTheme.BluePlanet;
        options.ShowSidebar = true;
    });
}

// Production Environment Security
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
    app.UseHsts();
}

app.Run();

public abstract partial class Program;