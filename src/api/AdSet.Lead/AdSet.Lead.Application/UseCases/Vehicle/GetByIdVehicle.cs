using AdSet.Lead.Application.DTOs;
using AdSet.Lead.Application.Mappers;
using AdSet.Lead.Domain.Repositories;

namespace AdSet.Lead.Application.UseCases.Vehicle;

public class GetByIdVehicle(IVehicleRepository repository)
{
    public async Task<GetByIdVehicleOutput> Execute(Guid id)
    {
        var vehicle = await repository.GetByIdAsync(id);

        var dto = VehicleMapper.ToDto(vehicle);

        return new GetByIdVehicleOutput(dto);
    }
}

public record GetByIdVehicleOutput(VehicleDto Vehicle);