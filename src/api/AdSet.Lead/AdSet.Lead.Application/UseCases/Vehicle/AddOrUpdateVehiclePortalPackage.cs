using AdSet.Lead.Application.DTOs;
using AdSet.Lead.Application.Mappers;
using AdSet.Lead.Domain.Enums;
using AdSet.Lead.Domain.Repositories;
using AdSet.Lead.Domain.VOs;

namespace AdSet.Lead.Application.UseCases.Vehicle;

public class AddOrUpdateVehiclePortalPackage(IVehicleRepository repository)
{
    public async Task<AddOrUpdateVehiclePortalPackageOutput> Execute(AddOrUpdateVehiclePortalPackageInput input)
    {
        var vehicle = await repository.GetByIdAsync(input.VehicleId);

        var portalPackage = new PortalPackage(input.Portal, input.Package);

        vehicle.AddOrUpdatePortalPackage(portalPackage);

        await repository.SaveAsync();

        return new AddOrUpdateVehiclePortalPackageOutput(
            vehicle.Id.ToString(),
            VehicleMapper.ToDto(vehicle)
        );
    }
}

public record AddOrUpdateVehiclePortalPackageInput(Guid VehicleId, Portal Portal, Package Package);

public record AddOrUpdateVehiclePortalPackageOutput(string Id, VehicleDto Vehicle);