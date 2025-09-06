using AdSet.Lead.Application.DTOs;
using AdSet.Lead.Application.Mappers;
using AdSet.Lead.Domain.Repositories;

namespace AdSet.Lead.Application.UseCases.Vehicle;

public class CreateVehicle(IVehicleRepository repository)
{
    public async Task<CreateVehicleOutput> Execute(CreateVehicleInput input)
    {
        var vehicle = VehicleMapper.FromDto(input.Vehicle);

        await repository.AddAsync(vehicle);
        await repository.SaveAsync();

        return new CreateVehicleOutput(vehicle.Id.ToString());
    }
}

public record CreateVehicleInput(VehicleDto Vehicle);

public record CreateVehicleOutput(string Id);