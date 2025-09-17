using AdSet.Lead.Application.DTOs;
using AdSet.Lead.Application.Interfaces;
using AdSet.Lead.Application.Mappers;
using AdSet.Lead.Domain.Entities;
using AdSet.Lead.Domain.Repositories;

namespace AdSet.Lead.Application.UseCases.Vehicle;

public class UploadVehiclePhoto(IVehicleRepository repository, IImageStorageService imageStorageService)
{
    public async Task<UploadVehiclePhotoOutput> Execute(UploadVehiclePhotoInput input)
    {
        var photoUrl = await imageStorageService.SaveImageAsync(
            input.FileStream,
            input.FileName,
            $"vehicles/{input.VehicleId}"
        );

        var vehicle = await repository.GetByIdAsync(input.VehicleId);

        var photo = new Photo(photoUrl);
        vehicle.AddPhoto(photo);

        await repository.AddPhotoAsync(photo);
        await repository.SaveAsync();

        return new UploadVehiclePhotoOutput(
            vehicle.Id.ToString(),
            VehicleMapper.ToDto(vehicle)
        );
    }
}

public record UploadVehiclePhotoInput(Guid VehicleId, Stream FileStream, string FileName);

public record UploadVehiclePhotoOutput(string Id, VehicleDto Vehicle);