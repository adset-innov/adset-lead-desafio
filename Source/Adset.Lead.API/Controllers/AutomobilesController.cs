using Adset.Lead.Application.Automobiles.CreateAutomobile;
using Adset.Lead.Application.Automobiles.DeleteAutomobile;
using Adset.Lead.Application.Automobiles.SearchAutomobile;
using Adset.Lead.Application.Automobiles.UpdateAutomobile;
using Adset.Lead.Domain.Abstractions;
using Adset.Lead.Domain.Automobiles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Adset.Lead.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AutomobilesController : ControllerBase
{
    private readonly ISender _sender;
    private readonly ILogger<AutomobilesController> _logger;

    public AutomobilesController(ISender sender, ILogger<AutomobilesController> logger)
    {
        _sender = sender;
        _logger = logger;
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
            FileNames: request.FileNames);

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
        try
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
                FileNames: request.FileNames);

            Result result = await _sender.Send(command, cancellationToken);

            return result.IsSuccess ? NoContent() : BadRequest(result.Error);
        }
        catch (Exception ex)
        {
            return BadRequest($"Internal server error: {ex.Message}");
        }
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

    [HttpPost("{id:guid}/images")]
    public async Task<IActionResult> AddImageToAutomobile(
        Guid id,
        [FromBody] AddImageRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            // Buscar o automóvel atual
            var searchQuery = new SearchAutomobileQuery(id);
            var searchResult = await _sender.Send(searchQuery, cancellationToken);

            if (searchResult.IsFailure)
                return BadRequest(searchResult.Error);

            var automobile = searchResult.Value.FirstOrDefault();
            if (automobile == null)
                return NotFound($"Automobile with ID {id} not found");

            // Preparar lista atual de fotos
            var currentPhotos = new List<string>();
            if (!string.IsNullOrEmpty(automobile.Photos))
            {
                try
                {
                    currentPhotos = System.Text.Json.JsonSerializer.Deserialize<List<string>>(automobile.Photos) ?? new List<string>();
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Failed to deserialize photos for automobile {AutomobileId}", id);
                    currentPhotos = new List<string>();
                }
            }

            // Extrair apenas o GUID do nome do arquivo (remover extensão)
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(request.FileName);

            // Adicionar nova imagem se não existir
            if (!string.IsNullOrEmpty(fileNameWithoutExtension) && !currentPhotos.Contains(fileNameWithoutExtension))
            {
                currentPhotos.Insert(0, fileNameWithoutExtension); // Adiciona no início da lista
            }
            else
            {
                _logger.LogWarning($"Não é possível adicionar imagem: fileName é nulo/vazio ou imagem '{request.FileName}' já existe na lista.");
            }

            // Atualizar o automóvel com as novas fotos
            var updateCommand = new UpdateAutomobileCommand(
                AutomobileId: id,
                Brand: automobile.Brand,
                Model: automobile.Model,
                Year: automobile.Year,
                Plate: automobile.Plate,
                Color: automobile.Color,
                Price: automobile.Price,
                Km: automobile.Km,
                Portal: ParsePortal(automobile.Portal),
                Package: ParsePackage(automobile.Package),
                OptionalFeatures: automobile.Features?.Select(f => (OptionalFeatures)f).ToList() ?? new List<OptionalFeatures>(),
                FileNames: currentPhotos);

            var updateResult = await _sender.Send(updateCommand, cancellationToken);

            return updateResult.IsSuccess
                ? Ok(new { message = "Imagem adicionada com sucesso", photos = currentPhotos })
                : BadRequest(updateResult.Error);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exceção em AddImageToAutomobile para automóvel {AutomobileId} com nome do arquivo {FileName}", id, request.FileName);
            return BadRequest($"Erro do Servidor Interno: {ex.Message}");
        }
    }

    private static Portal ParsePortal(string? portalString)
    {
        return portalString switch
        {
            "ICars" or "ICarros" => Portal.ICars,
            "WebMotors" => Portal.WebMotors,
            _ => Portal.WebMotors
        };
    }

    private static Package ParsePackage(string? packageString)
    {
        return packageString switch
        {
            "Bronze" => Package.Bronze,
            "Diamond" => Package.Diamond,
            "Platinum" => Package.Platinum,
            "Basic" => Package.Basic,
            _ => Package.Basic
        };
    }
}

public record AddImageRequest(string FileName);