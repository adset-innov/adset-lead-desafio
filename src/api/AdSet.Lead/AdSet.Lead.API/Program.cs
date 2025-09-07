using AdSet.Lead.Infrastructure.Data.Database;
using AdSet.Lead.Infrastructure.IoC;
using AdSet.Lead.Infrastructure.Settings;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Logging
builder.Host.UseSerilog((context, _, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext()
        .Enrich.WithEnvironmentName();
});

// Default Setup
builder.Services.AddControllers()
    .AddJsonOptions(opt => { opt.JsonSerializerOptions.PropertyNameCaseInsensitive = true; });

// Db Context
builder.Services.AddAppDbContext(builder.Configuration.GetConnectionString("DefaultConnection"));

// Services
builder.Services.AddInfrastructureServices();

// Repositories
builder.Services.AddRepositories();

// Use Cases
builder.Services.AddUseCases();

// OpenAPI
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

// Database Connection Test
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        var canConnect = await db.Database.CanConnectAsync();
        if (canConnect)
        {
            Log.Information("\e[32mDatabase connection established successfully.\e[0m");
        }
        else
        {
            Log.Warning("\e[33mDatabase connection failed: Database not accessible or does not exist.\e[0m");
        }
    }
    catch (Exception ex)
    {
        Log.Error(ex, "\e[31mDatabase connection failed with exception.\e[0m");
    }
}


// Middlewares
app.UseSerilogRequestLogging();

app.UseCors("DefaultCorsPolicy");

app.UseStaticFiles();

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

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
    app.UseHsts();
}

app.Run();

public abstract partial class Program;