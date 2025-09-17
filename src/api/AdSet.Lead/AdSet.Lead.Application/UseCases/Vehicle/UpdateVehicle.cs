using AdSet.Lead.Application.DTOs;
using AdSet.Lead.Application.Mappers;
using AdSet.Lead.Application.Services;
using AdSet.Lead.Domain.Repositories;

namespace AdSet.Lead.Application.UseCases.Vehicle;

public class UpdateVehicle(IVehicleRepository repository, VehicleOptionService optionService)
{
    public async Task<UpdateVehicleOutput> Execute(UpdateVehicleInput input)
    {
        var existing = await repository.GetByIdAsync(Guid.Parse(input.Id));

        var options = await optionService.ResolveOptionsAsync(input.Options);

        existing.UpdateDetails(
            input.Brand,
            input.Model,
            input.Year,
            input.LicensePlate,
            input.Color,
            input.Price,
            input.Mileage,
            options
        );

        await repository.SaveAsync();

        return new UpdateVehicleOutput(existing.Id.ToString());
    }
}

public record UpdateVehicleInput(
    string Id,
    string Brand,
    string Model,
    int Year,
    string LicensePlate,
    string Color,
    decimal Price,
    int Mileage,
    IEnumerable<string>? Options
);

public record UpdateVehicleOutput(string Id);