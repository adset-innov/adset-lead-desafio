using Adsetdesafio.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Connection string
var connectionString = builder.Configuration.GetConnectionString("AppDbConnectionString");

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "AdSetDesafio", Version = "v1" });
});

// EF Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Injeção de dependências customizadas
builder.Services.InjectRepositories();
builder.Services.InjectServices();

// Configurar CORS
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Configurar cookie authentication para APIs (sem redirecionar)
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = 401; // Unauthorized
        return Task.CompletedTask;
    };
    options.Events.OnRedirectToAccessDenied = context =>
    {
        context.Response.StatusCode = 403; // Forbidden
        return Task.CompletedTask;
    };
});

// Build da aplicação
var app = builder.Build();

// Swagger na dev
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AdsetDesafio V1");
    });
}

// Middleware pipeline
app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

// Autenticação + autorização (ordem importante)
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
