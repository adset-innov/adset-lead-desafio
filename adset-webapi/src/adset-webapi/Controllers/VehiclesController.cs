using AdSet.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace adset_webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiclesController : ControllerBase
    {
        [HttpPost("search")]
        public async Task<IResult> Search(
            [FromBody] SearchVehiclesFilter filter,
            [FromServices] ISearchVehicles searchVehicles,
            [FromQuery] int currentPage = 1,
            [FromQuery] int pageSize = 10)
        {
            var request = new SearchVehiclesRequest(filter, currentPage, pageSize);
            return Results.Ok(await searchVehicles.Execute(request));
        }

        [HttpPost]
        public async Task<IResult> Create(
            [FromForm] CreateUpdateVehicleViewModel vehiclesViewModel,
            [FromServices] ICreateVehicles createVehicles)
        {
            await createVehicles.Execute(vehiclesViewModel);
            return Results.Ok(new { Success = true, Message = "Vehicle saved!" });
        }

        [HttpPut("{id}")]
        public async Task<IResult> Update(
            [FromForm] int id,
            [FromBody] CreateUpdateVehicleViewModel vehiclesViewModel,
            [FromServices] IUpdateVehicles updateVehicles)
        {
            await updateVehicles.Execute(vehiclesViewModel);
            return Results.Ok(new { Success = true, Message = "Vehicle updated!" });
        }

        [HttpDelete]
        public async Task<IResult> Delete(
           [FromRoute] int vehicelId,
           [FromServices] IDeleteVehicles deleteVehicles)
        {
            await deleteVehicles.Execute(vehicelId);
            return Results.Ok(new { Success = true, Message = "Vehicle removed!" });
        }

    }
}
