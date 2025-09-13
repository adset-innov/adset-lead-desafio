using AdSet.Data.Repositories;
using AdSet.Domain.Interfaces;
using AdSet.Infra.Data;

namespace adset_webapi.Configuration
{
    public static class DefaultServiceExtensions
    {
        public static IServiceCollection AddDefaultServices(this IServiceCollection services)
            => services
            .AddContext()
            .AddUseCases()
            .AddRespositories();

        public static IServiceCollection AddContext(this IServiceCollection services)
            => services
            .AddDbContext<AdSetContext>(
                options =>
                {
                    options.UseSqlServer("name=ConnectionStrings:AdSetConnectionString");
                    options.EnableSensitiveDataLogging();
                }
            );

        public static IServiceCollection AddUseCases(this IServiceCollection services)
            => services
                .AddScoped<ISearchVehicles, SearchVehicles>()
                .AddScoped<ICreateVehicles, CreateVehicles>()
                .AddScoped<IDeleteVehicles, DeleteVehicles>()
                .AddScoped<IUpdateVehicles, UpdateVehicles>()
                .AddScoped<IUpdateVehiclePortalPackages, UpdateVehiclePortalPackages>()
                .AddScoped<IGetFilterOptions, GetFilterOptions>()

                //Optionals
                .AddScoped<IGetAllOptionals, GetAllOptionals>();



        public static IServiceCollection AddRespositories(this IServiceCollection services)
            => services
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IVehiclesRepository, VehiclesRepository>()
                .AddScoped<IVehicleOptionalRepository, VehicleOptionalRepository>()
                .AddScoped<IVehicleImageRepository, VehicleImageRepository>()
                .AddScoped<IVehiclePortalPackagesRepository, VehiclePortalPackagesRepository>()
                .AddScoped<IPortalRepository, PortalRepository>()
                .AddScoped<IPackagesRepository, PackagesRepository>()
                .AddScoped<IOptionalRepository, OptionalRepository>();
    }
}
