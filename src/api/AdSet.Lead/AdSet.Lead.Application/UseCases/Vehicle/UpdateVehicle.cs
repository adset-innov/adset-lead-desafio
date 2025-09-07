using AdSet.Lead.Application.DTOs;
using AdSet.Lead.Application.Mappers;
using AdSet.Lead.Domain.VOs;
using AdSet.Lead.Domain.Repositories;

namespace AdSet.Lead.Application.UseCases.Vehicle;

public class UpdateVehicle(IVehicleRepository repository)
{
    public async Task<UpdateVehicleOutput> Execute(UpdateVehicleInput input)
    {
        var existing = await repository.GetByIdAsync(Guid.Parse(input.Id));

        existing.UpdateDetails(
            input.Brand,
            input.Model,
            input.Year,
            input.LicensePlate,
            input.Color,
            input.Price,
            input.Mileage,
            new VehicleOptions(input.Options ?? new Dictionary<string, bool>())
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
    Dictionary<string, bool>? Options
);

public record UpdateVehicleOutput(string Id);