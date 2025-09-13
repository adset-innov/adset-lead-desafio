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
            [FromBody] SearchVehiclesFilter? filter,
            [FromServices] ISearchVehicles searchVehicles,
            [FromQuery] int currentPage = 1,
            [FromQuery] int pageSize = 10)
        {
            filter ??= new SearchVehiclesFilter();
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
            int id,
            [FromForm] CreateUpdateVehicleViewModel vehiclesViewModel,
            [FromServices] IUpdateVehicles updateVehicles)
        {
            vehiclesViewModel.Id = id;
            await updateVehicles.Execute(vehiclesViewModel);
            return Results.Ok(new { Success = true, Message = "Vehicle updated!" });
        }

        [HttpDelete("{id:int}")]
        public async Task<IResult> Delete(
           int id,
           [FromServices] IDeleteVehicles deleteVehicles)
        {
            await deleteVehicles.Execute(id);
            return Results.Ok(new { Success = true, Message = "Vehicle removed!" });
        }

    }
}
