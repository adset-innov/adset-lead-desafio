using ADSET.DESAFIO.APPLICATION.Handlers.Commands;
using ADSET.DESAFIO.APPLICATION.Handlers.Queries;
using ADSET.DESAFIO.DOMAIN.Interfaces;
using ADSET.DESAFIO.INFRASTRUCTURE.IOC.Profiles;
using ADSET.DESAFIO.INFRASTRUCTURE.Persistence;
using ADSET.DESAFIO.INFRASTRUCTURE.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ADSET.DESAFIO.INFRASTRUCTURE.IOC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddGeneralInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("MasterDbConnection"),
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(10),
                            errorNumbersToAdd: null
                        );
                    }
                )
            );

            services.AddAutoMapper(cfg => { cfg.AddProfile<MappingProfile>(); }, typeof(MappingProfile).Assembly);

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(DeleteCarCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(RegisterCarCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(UpdateCarCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetAllCarQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetCarByIdQuery).Assembly);
            });

            services.AddScoped<ICarRepository, CarRepository>();

            return services;
        }
    }
}