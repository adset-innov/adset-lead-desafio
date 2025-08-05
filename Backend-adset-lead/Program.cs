using Backend_adset_lead.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var server = builder.Configuration["DatabaseConfig:Server"];
var database = builder.Configuration["DatabaseConfig:Database"];
var trustedConnection = builder.Configuration["DatabaseConfig:Trusted_Connection"];
var trustServerCertificate = builder.Configuration["DatabaseConfig:TrustServerCertificate"];

var connectionString = $"Server={server};Database={database};Trusted_Connection={trustedConnection};TrustServerCertificate={trustServerCertificate};";

builder.Services.AddDbContext<AdsetLeadContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.

builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
