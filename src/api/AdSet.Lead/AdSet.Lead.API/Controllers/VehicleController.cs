using AdSet.Lead.Application.DTOs;
using AdSet.Lead.Application.UseCases.Vehicle;
using AdSet.Lead.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace AdSet.Lead.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VehicleController(
    CreateVehicle createVehicleUc,
    DeleteVehicle deleteVehicleUc,
    GetAllVehicles getAllVehiclesUc,
    GetByIdVehicle getByIdVehicleUc,
    UpdateVehicle updateVehicleUc,
    SearchVehicles searchVehiclesUc,
    GetTotalCountVehicle getTotalCountVehicleUc,
    GetWithPhotosCountVehicle getWithPhotosCountVehicleUc,
    GetWithoutPhotosCountVehicle getWithoutPhotosCountVehicleUc,
    GetDistinctColorsVehicle getDistinctColorsVehicleUc,
    UploadVehiclePhoto uploadVehiclePhotoUc,
    RemoveVehiclePhoto removeVehiclePhotoUc,
    AddOrUpdateVehiclePortalPackage addOrUpdateVehiclePortalPackageUc,
    RemoveVehiclePortalPackage removeVehiclePortalPackageUc
) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateVehicleInput req)
    {
        var output = await createVehicleUc.Execute(req);
        return Ok(output);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var output = await deleteVehicleUc.Execute(id);
        return Ok(output);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var output = await getAllVehiclesUc.Execute();
        return Ok(output);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var output = await getByIdVehicleUc.Execute(id);
        return Ok(output);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] VehicleDto req)
    {
        var input = new UpdateVehicleInput(id.ToString(), req);
        var output = await updateVehicleUc.Execute(input);
        return Ok(output);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] SearchVehiclesInput req)
    {
        var output = await searchVehiclesUc.Execute(req);
        return Ok(output);
    }

    [HttpGet("count/total")]
    public async Task<IActionResult> GetTotalCount()
    {
        var output = await getTotalCountVehicleUc.Execute();
        return Ok(output);
    }

    [HttpGet("count/with-photos")]
    public async Task<IActionResult> GetWithPhotosCount()
    {
        var output = await getWithPhotosCountVehicleUc.Execute();
        return Ok(output);
    }

    [HttpGet("count/without-photos")]
    public async Task<IActionResult> GetWithoutPhotosCount()
    {
        var output = await getWithoutPhotosCountVehicleUc.Execute();
        return Ok(output);
    }

    [HttpGet("colors")]
    public async Task<IActionResult> GetDistinctColors()
    {
        var output = await getDistinctColorsVehicleUc.Execute();
        return Ok(output);
    }

    [HttpPost("{id:guid}/photos")]
    public async Task<IActionResult> UploadPhoto(Guid id, IFormFile? file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Invalid file.");

        if (file.Length > 5 * 1024 * 1024)
            return BadRequest("File size cannot exceed 5MB.");

        await using var stream = file.OpenReadStream();

        var input = new UploadVehiclePhotoInput(id, stream, file.FileName);

        var output = await uploadVehiclePhotoUc.Execute(input);

        return Ok(output);
    }

    [HttpDelete("{id:guid}/photos/{photoId:guid}")]
    public async Task<IActionResult> RemovePhoto(Guid id, Guid photoId)
    {
        var input = new RemoveVehiclePhotoInput(id, photoId);
        var output = await removeVehiclePhotoUc.Execute(input);
        return Ok(output);
    }

    [HttpPost("{id:guid}/portal-package")]
    public async Task<IActionResult> AddOrUpdatePortalPackage(Guid id,
        [FromBody] AddOrUpdateVehiclePortalPackageInput body)
    {
        var input = body with { VehicleId = id };
        var output = await addOrUpdateVehiclePortalPackageUc.Execute(input);
        return Ok(output);
    }

    [HttpDelete("{id:guid}/portal-package/{portal}")]
    public async Task<IActionResult> RemovePortalPackage(Guid id, Portal portal)
    {
        var input = new RemoveVehiclePortalPackageInput(id, portal);
        var output = await removeVehiclePortalPackageUc.Execute(input);
        return Ok(output);
    }
}