using AdSet.Lead.Application.Interfaces;
using AdSet.Lead.Application.Services;
using AdSet.Lead.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AdSet.Lead.Infrastructure.IoC;

public static class AddApplicationServiceExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<VehicleOptionService>();

        return services;
    }
}