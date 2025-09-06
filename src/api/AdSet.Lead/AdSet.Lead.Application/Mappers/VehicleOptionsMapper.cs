using AdSet.Lead.Application.DTOs;
using AdSet.Lead.Domain.VOs;

namespace AdSet.Lead.Application.Mappers;

public static class VehicleOptionsMapper
{
    public static VehicleOptionsDto ToDto(VehicleOptions options)
    {
        return new VehicleOptionsDto(
            options.AirConditioning,
            options.Alarm,
            options.Airbag,
            options.AbsBrakes
        );
    }

    public static VehicleOptions FromDto(VehicleOptionsDto dto)
    {
        return new VehicleOptions(
            dto.AirConditioning,
            dto.Alarm,
            dto.Airbag,
            dto.AbsBrakes
        );
    }
}