using ADSET.DESAFIO.INFRASTRUCTURE.IOC;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddGeneralInfrastructure(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddOpenApi();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
