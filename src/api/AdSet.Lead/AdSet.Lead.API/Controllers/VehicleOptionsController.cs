using AdSet.Lead.Application.UseCases.VehicleOption;
using Microsoft.AspNetCore.Mvc;

namespace AdSet.Lead.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VehicleOptionsController(
    CreateVehicleOption createVehicleOptionUc,
    SearchVehicleOptions searchVehicleOptionsUc
) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateVehicleOptionInput input)
    {
        var output = await createVehicleOptionUc.Execute(input);
        return Ok(output);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string query)
    {
        var output = await searchVehicleOptionsUc.Execute(query);
        return Ok(output);
    }
}