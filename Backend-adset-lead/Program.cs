using Backend_adset_lead.Contexts;
using Backend_adset_lead.Repositories;
using Backend_adset_lead.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var server = builder.Configuration["DatabaseConfig:Server"];
var database = builder.Configuration["DatabaseConfig:Database"];
var trustedConnection = builder.Configuration["DatabaseConfig:Trusted_Connection"];
var trustServerCertificate = builder.Configuration["DatabaseConfig:TrustServerCertificate"];

var connectionString = $"Server={server};Database={database};Trusted_Connection={trustedConnection};TrustServerCertificate={trustServerCertificate};";

builder.Services.AddDbContext<AdsetLeadContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost4200", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICarroRepository, CarroRepository>();
builder.Services.AddScoped<ICarroService, CarroService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowLocalhost4200");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
