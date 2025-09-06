using AdSet.Lead.Domain.Enums;

namespace AdSet.Lead.Application.DTOs;

public record PortalPackageDto(
    Portal Portal,
    Package Package
);