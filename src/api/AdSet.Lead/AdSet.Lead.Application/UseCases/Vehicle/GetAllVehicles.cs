using AdSet.Lead.Application.DTOs;
using AdSet.Lead.Application.Mappers;
using AdSet.Lead.Domain.Repositories;

namespace AdSet.Lead.Application.UseCases.Vehicle;

public class GetAllVehicles(IVehicleRepository repository)
{
    public async Task<GetAllVehiclesOutput> Execute()
    {
        var vehicles = await repository.GetAllAsync();

        var dtos = vehicles.Select(VehicleMapper.ToDto).ToList();

        return new GetAllVehiclesOutput(dtos);
    }
}

public record GetAllVehiclesOutput(List<VehicleDto> Vehicles);