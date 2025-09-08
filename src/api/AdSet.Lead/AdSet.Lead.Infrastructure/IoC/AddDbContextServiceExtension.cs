using AdSet.Lead.Infrastructure.Data.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AdSet.Lead.Infrastructure.IoC;

public static class DbContextServiceExtension
{
    public static IServiceCollection AddAppDbContext(
        this IServiceCollection services,
        string? connectionString
    )
    {
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new ArgumentException(
                "Database Connection String was not found.",
                nameof(connectionString)
            );
        }

        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

        return services;
    }
}