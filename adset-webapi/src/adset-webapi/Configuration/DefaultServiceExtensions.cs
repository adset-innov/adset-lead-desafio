using AdSet.Data.Repositories;
using adset_webapi.Mappings; 

namespace adset_webapi.Configuration
{
    public static class DefaultServiceExtensions
    {
        public static IServiceCollection AddDefaultServices(this IServiceCollection services)
            => services
            .AddContext()
            .AddUseCases()
            .AddRespositories();
            //.AddViewModelMapper();

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
                .AddScoped<ISearchVehicles, SearchVehicles>();

       public static IServiceCollection AddRespositories(this IServiceCollection services)
            => services
                .AddScoped<IVehiclesRepository, VehiclesRepository>();

        //public static IServiceCollection AddViewModelMapper(this IServiceCollection services)
        //{
        //    services.AddAutoMapper(typeof(ViewModelMappingProfile).Assembly);
        //    return services;
        //}
    
    }


}
