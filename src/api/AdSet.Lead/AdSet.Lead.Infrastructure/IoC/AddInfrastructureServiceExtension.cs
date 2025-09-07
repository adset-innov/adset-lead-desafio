using AdSet.Lead.Application.Interfaces;
using AdSet.Lead.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AdSet.Lead.Infrastructure.IoC;

public static class AddInfrastructureServiceExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IImageStorageService, LocalImageStorageService>();

        return services;
    }
}