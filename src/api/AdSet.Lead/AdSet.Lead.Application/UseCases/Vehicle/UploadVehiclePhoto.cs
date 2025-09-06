using AdSet.Lead.Application.DTOs;
using AdSet.Lead.Application.Mappers;
using AdSet.Lead.Domain.Entities;
using AdSet.Lead.Domain.Repositories;

namespace AdSet.Lead.Application.UseCases.Vehicle;

public class UploadVehiclePhoto(IVehicleRepository repository)
{
    public async Task<UploadVehiclePhotoOutput> Execute(UploadVehiclePhotoInput input)
    {
        if (input.FileStream == null || input.FileStream.Length == 0)
            throw new ArgumentException("Invalid file.");

        var uploadsFolder = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot", "uploads", "vehicles", input.VehicleId.ToString()
        );

        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var newFileName = $"{Guid.NewGuid()}{Path.GetExtension(input.FileName)}";
        var filePath = Path.Combine(uploadsFolder, newFileName);

        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await input.FileStream.CopyToAsync(stream);
        }

        var photoUrl = $"/uploads/vehicles/{input.VehicleId}/{newFileName}";

        var vehicle = await repository.GetByIdAsync(input.VehicleId);
        vehicle.AddPhoto(new Photo(photoUrl));
        await repository.SaveAsync();

        return new UploadVehiclePhotoOutput(
            vehicle.Id.ToString(),
            VehicleMapper.ToDto(vehicle)
        );
    }
}

public record UploadVehiclePhotoInput(Guid VehicleId, Stream FileStream, string FileName);

public record UploadVehiclePhotoOutput(string Id, VehicleDto Vehicle);