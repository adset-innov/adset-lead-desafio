namespace adset_webapi.Controllers
{
    public static class VehiclesEndpoints
    {
        public static WebApplication MapVehiclesEndpoints(this WebApplication app)
        {
            var routeGroup = app
                .MapGroup("app/vehicles");
            routeGroup.MapGet("", GetVehicles);

           return app;
        }

        public static async Task<IResult> GetVehicles(IGetVehicles getVehicles)
            => Results.Ok(await getVehicles.Execute());
    }
  
}
