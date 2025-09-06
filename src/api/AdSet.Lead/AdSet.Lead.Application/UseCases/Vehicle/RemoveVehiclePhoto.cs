using AdSet.Lead.Domain.Repositories;

namespace AdSet.Lead.Application.UseCases.Vehicle;

public class RemoveVehiclePhoto(IVehicleRepository repository)
{
    public async Task<RemoveVehiclePhotoOutput> Execute(RemoveVehiclePhotoInput input)
    {
        var vehicle = await repository.GetByIdAsync(input.VehicleId);
        var photo = vehicle.Photos.FirstOrDefault(p => p.Id == input.PhotoId);
        if (photo == null)
            throw new ArgumentException("Photo not found.");

        var uploadsFolder = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot", "uploads", "vehicles", input.VehicleId.ToString()
        );

        var fileName = Path.GetFileName(photo.Url);
        var filePath = Path.Combine(uploadsFolder, fileName);

        if (File.Exists(filePath))
            File.Delete(filePath);

        vehicle.RemovePhoto(input.PhotoId);
        await repository.SaveAsync();

        return new RemoveVehiclePhotoOutput(vehicle.Id.ToString());
    }
}

public record RemoveVehiclePhotoInput(Guid VehicleId, Guid PhotoId);

public record RemoveVehiclePhotoOutput(string VehicleId);