using AdSet.Lead.Application.DTOs;
using AdSet.Lead.Domain.Entities;

namespace AdSet.Lead.Application.Mappers;

public static class PhotoMapper
{
    public static PhotoDto ToDto(Photo photo)
    {
        return new PhotoDto(
            photo.Id.ToString(),
            photo.Url
        );
    }

    public static Photo FromDto(PhotoDto dto)
    {
        return new Photo(
            dto.Url
        );
    }
}