using Microsoft.AspNetCore.Mvc;

namespace adset_webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiclesEndpoints : ControllerBase
    {
        [HttpPost]
        public async Task<IResult> SearchVehicles(
            [FromBody] SearchVehiclesFilter filter,
            [FromServices] ISearchVehicles searchVehicles,
            [FromQuery] int currentPage = 1,
            [FromQuery] int pageSize = 10)
        {
            var request = new SearchVehiclesRequest(filter, currentPage, pageSize);
            return Results.Ok(await searchVehicles.Execute(request));
        }
    }
}
