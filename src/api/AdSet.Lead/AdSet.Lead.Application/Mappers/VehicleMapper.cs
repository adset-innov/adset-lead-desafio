using AdSet.Lead.Application.DTOs;
using AdSet.Lead.Domain.Entities;

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
            VehicleOptionsMapper.ToDto(vehicle.Options),
            vehicle.Photos.Select(PhotoMapper.ToDto).ToList(),
            vehicle.PortalPackages.Select(PortalPackageMapper.ToDto).ToList()
        );
    }

    public static Vehicle FromDto(VehicleDto dto)
    {
        var options = VehicleOptionsMapper.FromDto(dto.Options);

        var photos = dto.Photos?
            .Select(PhotoMapper.FromDto)
            .ToList();

        var portalPackages = dto.PortalPackages?
            .Select(PortalPackageMapper.FromDto)
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