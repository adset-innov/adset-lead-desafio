using Adset.Lead.Application.Automobiles.CreateAutomobile;
using Adset.Lead.Application.Automobiles.DeleteAutomobile;
using Adset.Lead.Application.Automobiles.SearchAutomobile;
using Adset.Lead.Application.Automobiles.UpdateAutomobile;
using Adset.Lead.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Adset.Lead.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AutomobilesController : ControllerBase
{
    private readonly ISender _sender;

    public AutomobilesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAutomobileAsync(Guid id, CancellationToken cancellationToken)
    {
        var query = new SearchAutomobileQuery(id);

        Result<IReadOnlyList<SearchAutomobileResponse>> result = await _sender.Send(query, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        var automobile = result.Value.FirstOrDefault();

        if (automobile == null)
            return NotFound($"Automobile with ID {id} not found");
        
        return Ok(automobile);
    }
    
    [HttpGet]
    public async Task<IActionResult> SearchAutomobiles(
        [FromQuery] SearchAutomobilesRequest request,
        CancellationToken cancellationToken = default)
    {
        var query = new SearchAutomobileQuery(
            Plate: request.Plate,
            Brand: request.Brand,
            Model: request.Model,
            YearMin: request.YearMin,
            YearMax: request.YearMax,
            PriceMin: request.PriceMin,
            PriceMax: request.PriceMax,
            Color: request.Color,
            HasPhotos: request.HasPhotos,
            Portal: request.Portal,
            Package: request.Package,
            Feature: request.Feature);

        Result<IReadOnlyList<SearchAutomobileResponse>> result = await _sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(new
            {
                Error = result.Error.Code,
                Message = result.Error.Name
            });
        }

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAutomobile([FromBody] CreateAutomobileRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateAutomobileCommand(
            Brand: request.Brand,
            Model: request.Model,
            Year: request.Year,
            Plate: request.Plate,
            Color: request.Color,
            Price: request.Price,
            Km: request.Km,
            Portal: request.Portal,
            Package: request.Package,
            OptionalFeatures: request.OptionalFeatures,
            PhotoUrls: request.PhotoUrls);

        Result<Guid> result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(new
            {
                Error = result.Error.Code,
                Message = result.Error.Name
            });
        }

        return Ok(result.Value);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAutomobile(
        Guid id,
        [FromBody] UpdateAutomobileRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateAutomobileCommand(
            AutomobileId: id,
            Brand: request.Brand,
            Model: request.Model,
            Year: request.Year,
            Plate: request.Plate,
            Color: request.Color,
            Price: request.Price,
            Km: request.Km,
            Portal: request.Portal,
            Package: request.Package,
            OptionalFeatures: request.OptionalFeatures,
            PhotoUrls: request.PhotoUrls);

        Result result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess ? NoContent() : BadRequest(result.Error);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAutomobile(
        Guid id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteAutomobileCommand(id);

        Result result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess ? NoContent() : BadRequest(result.Error);
    }
}