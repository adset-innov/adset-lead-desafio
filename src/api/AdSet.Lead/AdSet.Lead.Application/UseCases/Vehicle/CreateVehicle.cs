using AdSet.Lead.Application.DTOs;
using AdSet.Lead.Application.Interfaces;
using AdSet.Lead.Application.Mappers;
using AdSet.Lead.Domain.Entities;
using AdSet.Lead.Domain.Repositories;

namespace AdSet.Lead.Application.UseCases.Vehicle;

public class CreateVehicle(IVehicleRepository repository, IImageStorageService imageStorageService)
{
    public async Task<CreateVehicleOutput> Execute(CreateVehicleInput input)
    {
        var photos = await SavePhotosAsync(input.Files);

        var vehicle = new Domain.Entities.Vehicle(
            input.Brand,
            input.Model,
            input.Year,
            input.LicensePlate,
            input.Color,
            input.Price,
            input.Mileage,
            VehicleOptionsMapper.FromDto(input.Options),
            photos,
            input.PortalPackages?.Select(PortalPackageMapper.FromDto)
        );

        await repository.AddAsync(vehicle);
        await repository.SaveAsync();

        return new CreateVehicleOutput(vehicle.Id.ToString());
    }

    private async Task<List<Photo>> SavePhotosAsync(List<CreateVehicleFile>? files)
    {
        var photos = new List<Photo>();

        if (files is null || files.Count == 0)
            return photos;

        foreach (var file in files)
        {
            var photoUrl = await imageStorageService.SaveImageAsync(file.FileStream, file.FileName, "vehicles");
            photos.Add(new Photo(photoUrl));
        }

        return photos;
    }
}

public record CreateVehicleInput(
    string Brand,
    string Model,
    int Year,
    string LicensePlate,
    string Color,
    decimal Price,
    int Mileage,
    VehicleOptionsDto Options,
    List<CreateVehicleFile>? Files,
    List<PortalPackageDto>? PortalPackages
);

public record CreateVehicleFile(Stream FileStream, string FileName);

public record CreateVehicleOutput(string Id);