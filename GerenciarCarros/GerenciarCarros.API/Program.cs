using GerenciarCarros.Application.Interfaces;
using GerenciarCarros.Application.Services;
using GerenciarCarros.Domain.Repositories;
using GerenciarCarros.Infrastructure.Context;
using GerenciarCarros.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<CarrosDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//builder.Services.AddScoped<CarrosDBContext>();//builder.Services.AddScoped<CarrosDBContext>();//builder.Services.AddScoped<CarrosDBContext>();
builder.Services.AddScoped<ICarroRepository, CarroRepository>();
builder.Services.AddScoped<IImagemRepository, ImagemRepository>();
builder.Services.AddScoped<IOpcionalRepository, OpcionalRepository>();

builder.Services.AddScoped<ICarroService, CarroService>();
builder.Services.AddScoped<IOptionalService, OptionalService>();
builder.Services.AddScoped<IImagemService, ImagemService>();
builder.Services.AddScoped<IPacoteRepository, PacoteRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Development",
        builder =>
            builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});
// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
}); ;
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


app.UseCors("Development");
app.UseHsts();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
