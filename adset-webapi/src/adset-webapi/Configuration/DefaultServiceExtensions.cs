namespace adset_webapi.Configuration
{
    public static class DefaultServiceExtensions
    {
        public static IServiceCollection AddDefaultServices(this IServiceCollection services)
            => services
            .AddContext();

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
            .AddScoped<IGetVehicles, GetVehicles>(); 
    }
}
