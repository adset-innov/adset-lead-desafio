using API.Contexto;
using API.Mappings;
using API.Middlewares;
using API.Repositorios.Implementacoes;
using API.Repositorios.Interfaces;
using API.Services.Implementacoes;
using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;


string wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
string uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

if (!Directory.Exists(wwwrootPath))
{
    Directory.CreateDirectory(wwwrootPath);
}

if (!Directory.Exists(uploadsPath))
{
    Directory.CreateDirectory(uploadsPath);
}

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AdSetContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AdSetConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AdSet API Veiculos",
        Version = "v1",
        Description = "API de gestão de veículos para resolução de desafio técnico"
    });
});

builder.Services.AddScoped<IVeiculoService, VeiculoService>();
builder.Services.AddScoped<IPortalService, PortalService>();
builder.Services.AddScoped<IFotoService, FotoService>();
builder.Services.AddScoped<IVeiculoRepository, VeiculoRepository>();
builder.Services.AddScoped<IPortalRepository, PortalRepository>();
builder.Services.AddScoped<IFotoRepository, FotoRepository>();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile(new VeiculoProfile());
    cfg.AddProfile(new FotoProfile());
    cfg.AddProfile(new OpcionalProfile());
    cfg.AddProfile(new PacoteProfile());
    cfg.AddProfile(new PortalProfile());
    cfg.AddProfile(new RelacaoVeiculoOpcionalProfile());
    cfg.AddProfile(new RelacaoVeiculoPacotePortalProfile());

});

var app = builder.Build();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "Uploads")
    ),
    RequestPath = "/uploads" 
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "AdSet API");
        options.RoutePrefix = string.Empty;
    });
}

app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseAuthorization();
app.MapControllers();
app.Run();
