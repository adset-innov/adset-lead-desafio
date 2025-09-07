using System.Text.Json;
using System.Text.Json.Serialization;
using AdSet.Lead.API.Binders;
using AdSet.Lead.Application.DTOs;
using AdSet.Lead.Application.UseCases.Vehicle;
using AdSet.Lead.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using AdSet.Lead.API.Helpers;
using AdSet.Lead.Domain.Filters;

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
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Create(
        [FromForm] string brand,
        [FromForm] string model,
        [FromForm] int year,
        [FromForm] string licensePlate,
        [FromForm] string color,
        [FromForm] decimal price,
        [FromForm] int mileage,
        [FromForm] string options, // JSON string vindo do form
        [FromForm] List<IFormFile>? files,
        [FromForm] string? portalPackages
    )
    {
        var error = ImageValidator.Validate(files);
        if (error != null)
            return BadRequest(error);

        if (!TryDeserialize(options, out Dictionary<string, bool>? optionsDict, out var optionsError))
            return BadRequest($"Invalid 'Options' JSON: {optionsError}");

        List<PortalPackageDto>? portalPackagesDto = null;
        if (!string.IsNullOrWhiteSpace(portalPackages) &&
            !TryDeserialize(portalPackages, out portalPackagesDto, out var portalError))
        {
            return BadRequest($"Invalid 'PortalPackages' JSON: {portalError}");
        }

        var fileInputs = files?.Select(f => new CreateVehicleFile(f.OpenReadStream(), f.FileName)).ToList();

        var input = new CreateVehicleInput(
            brand,
            model,
            year,
            licensePlate,
            color,
            price,
            mileage,
            optionsDict ?? new Dictionary<string, bool>(), // ✅ agora direto
            fileInputs,
            portalPackagesDto
        );

        var output = await createVehicleUc.Execute(input);
        return Ok(output);
    }

    private static bool TryDeserialize<T>(string json, out T? result, out string? error)
    {
        try
        {
            result = JsonSerializer.Deserialize<T>(json, JsonOptions);
            error = null;
            return true;
        }
        catch (JsonException ex)
        {
            result = default;
            error = ex.Message;
            return false;
        }
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
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateVehicleInput req)
    {
        var input = req with { Id = id.ToString() };
        var output = await updateVehicleUc.Execute(input);
        return Ok(output);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search(
        [FromQuery] string? plate,
        [FromQuery] string? brand,
        [FromQuery] string? model,
        [FromQuery] int? yearMin,
        [FromQuery] int? yearMax,
        [FromQuery] decimal? priceMin,
        [FromQuery] decimal? priceMax,
        [FromQuery] bool? hasPhotos,
        [FromQuery] string? color,
        [FromQueryBinder<VehicleOptionsFilterBinder>] VehicleOptionsFilter? options,
        [FromQuery] Portal? portal,
        [FromQuery] Package? package,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10
    )
    {
        var input = new SearchVehiclesInput(
            plate, brand, model, yearMin, yearMax,
            priceMin, priceMax, hasPhotos, color,
            options, portal, package, pageNumber, pageSize
        );

        var output = await searchVehiclesUc.Execute(input);
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
        var error = ImageValidator.Validate(file);
        if (error != null)
            return BadRequest(error);

        await using var stream = file!.OpenReadStream();

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
 