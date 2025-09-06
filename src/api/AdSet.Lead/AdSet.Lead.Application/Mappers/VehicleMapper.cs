using AdSet.Lead.Application.DTOs;
using AdSet.Lead.Domain.Entities;
using AdSet.Lead.Domain.VOs;

namespace AdSet.Lead.Application.Mappers;

public static class VehicleMapper
{
    public static VehicleDto ToDto(Vehicle vehicle)
    {
        return new VehicleDto(
            vehicle.Id.ToString(),
            vehicle.CreatedOn,
            vehicle.UpdatedOn,
            vehicle.Brand,
            vehicle.Model,
            vehicle.Year,
            vehicle.LicensePlate.Value,
            vehicle.Color.Value,
            vehicle.Price,
            vehicle.Mileage,
            new VehicleOptionsDto(
                vehicle.Options.AirConditioning,
                vehicle.Options.Alarm,
                vehicle.Options.Airbag,
                vehicle.Options.AbsBrakes
            ),
            vehicle.Photos.Select(p => new PhotoDto(p.Id.ToString(), p.Url)).ToList(),
            vehicle.PortalPackages.Select(pp => new PortalPackageDto(pp.Portal, pp.Package)).ToList()
        );
    }

    public static Vehicle FromDto(VehicleDto dto)
    {
        var options = new VehicleOptions(
            dto.Options.AirConditioning,
            dto.Options.Alarm,
            dto.Options.Airbag,
            dto.Options.AbsBrakes
        );

        var photos = dto.Photos?
            .Select(p => new Photo(p.Url))
            .ToList();

        var portalPackages = dto.PortalPackages?
            .Select(pp => new PortalPackage(pp.Portal, pp.Package))
            .ToList();

        return new Vehicle(
            dto.Brand,
            dto.Model,
            dto.Year,
            dto.LicensePlate,
            dto.Color,
            dto.Price,
            dto.Mileage,
            options,
            photos,
            portalPackages
        );
    }
}