using AdSet.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace adset_webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiclePortalPackagesController : ControllerBase
    {
        [HttpPost("update")]
        public async Task<IResult> Update(
            [FromBody] UpdateVehiclePortalPackagesViewModel model,
            [FromServices] IUpdateVehiclePortalPackages updateVehiclePortalPackages)
        { 
            await updateVehiclePortalPackages.Execute(model);
            return Results.Ok(new { Success = true, Message = "Vehicle packages successfully updated!" });
        }
    }
}
