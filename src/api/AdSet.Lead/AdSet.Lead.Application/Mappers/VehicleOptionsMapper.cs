using AdSet.Lead.Application.DTOs;
using AdSet.Lead.Domain.VOs;

namespace AdSet.Lead.Application.Mappers;

public static class VehicleOptionsMapper
{
    public static VehicleOptionsDto ToDto(VehicleOptions options)
    {
        return new VehicleOptionsDto(new Dictionary<string, bool>(options.Options));
    }

    public static VehicleOptions FromDto(VehicleOptionsDto dto)
    {
        return new VehicleOptions(new Dictionary<string, bool>(dto.Options));
    }
}