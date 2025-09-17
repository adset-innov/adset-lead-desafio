using AdSet.Lead.Application.Interfaces;
using AdSet.Lead.Domain.Repositories;

namespace AdSet.Lead.Application.UseCases.Vehicle;

public class RemoveVehiclePhoto(IVehicleRepository repository, IImageStorageService imageStorageService)
{
    public async Task<RemoveVehiclePhotoOutput> Execute(RemoveVehiclePhotoInput input)
    {
        var vehicle = await repository.GetByIdAsync(input.VehicleId);

        var photo = vehicle.Photos.FirstOrDefault(p => p.Id == input.PhotoId);
        if (photo == null)
            throw new ArgumentException("Photo not found.");

        imageStorageService.DeleteImage(photo.Url);

        vehicle.RemovePhoto(input.PhotoId);

        await repository.RemovePhotoAsync(photo);

        await repository.SaveAsync();

        return new RemoveVehiclePhotoOutput(vehicle.Id.ToString(), input.PhotoId.ToString());
    }
}

public record RemoveVehiclePhotoInput(Guid VehicleId, Guid PhotoId);

public record RemoveVehiclePhotoOutput(string VehicleId, string PhotoId);