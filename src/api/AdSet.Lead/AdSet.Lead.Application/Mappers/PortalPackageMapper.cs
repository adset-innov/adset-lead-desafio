using AdSet.Lead.Application.DTOs;
using AdSet.Lead.Domain.VOs;

namespace AdSet.Lead.Application.Mappers;

public static class PortalPackageMapper
{
    public static PortalPackageDto ToDto(PortalPackage portalPackage)
    {
        return new PortalPackageDto(
            portalPackage.Portal,
            portalPackage.Package
        );
    }

    public static PortalPackage FromDto(PortalPackageDto dto)
    {
        return new PortalPackage(
            dto.Portal,
            dto.Package
        );
    }
}