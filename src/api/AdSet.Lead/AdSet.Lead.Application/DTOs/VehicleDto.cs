using AdSet.Lead.Domain.VOs;

namespace AdSet.Lead.Application.DTOs;

public record VehicleDto(
    string Id,
    DateTime CreatedOn,
    DateTime UpdatedOn,
    string Brand,
    string Model,
    int Year,
    string LicensePlate,
    string Color,
    decimal Price,
    int Mileage,
    VehicleOptionsDto Options,
    List<PhotoDto> Photos,
    List<PortalPackageDto> PortalPackages
);