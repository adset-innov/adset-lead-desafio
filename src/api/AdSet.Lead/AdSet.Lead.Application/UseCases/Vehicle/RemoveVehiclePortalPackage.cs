using AdSet.Lead.Application.DTOs;
using AdSet.Lead.Application.Mappers;
using AdSet.Lead.Domain.Enums;
using AdSet.Lead.Domain.Repositories;

namespace AdSet.Lead.Application.UseCases.Vehicle;

public class RemoveVehiclePortalPackage(IVehicleRepository repository)
{
    public async Task<RemoveVehiclePortalPackageOutput> Execute(RemoveVehiclePortalPackageInput input)
    {
        var vehicle = await repository.GetByIdAsync(input.VehicleId);

        vehicle.RemovePortalPackage(input.Portal);

        await repository.SaveAsync();

        return new RemoveVehiclePortalPackageOutput(
            vehicle.Id.ToString(),
            VehicleMapper.ToDto(vehicle)
        );
    }
}

public record RemoveVehiclePortalPackageInput(Guid VehicleId, Portal Portal);

public record RemoveVehiclePortalPackageOutput(string Id, VehicleDto Vehicle);