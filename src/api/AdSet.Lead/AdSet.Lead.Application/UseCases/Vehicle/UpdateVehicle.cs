using AdSet.Lead.Application.DTOs;
using AdSet.Lead.Application.Mappers;
using AdSet.Lead.Domain.Repositories;

namespace AdSet.Lead.Application.UseCases.Vehicle;

public class UpdateVehicle(IVehicleRepository repository)
{
    public async Task<UpdateVehicleOutput> Execute(UpdateVehicleInput input)
    {
        var existing = await repository.GetByIdAsync(Guid.Parse(input.Id));

        existing.UpdateDetails(
            input.Vehicle.Brand,
            input.Vehicle.Model,
            input.Vehicle.Year,
            input.Vehicle.LicensePlate,
            input.Vehicle.Color,
            input.Vehicle.Price,
            input.Vehicle.Mileage,
            VehicleOptionsMapper.FromDto(input.Vehicle.Options)
        );

        await repository.SaveAsync();

        return new UpdateVehicleOutput(existing.Id.ToString());
    }
}

public record UpdateVehicleInput(string Id, VehicleDto Vehicle);

public record UpdateVehicleOutput(string Id);