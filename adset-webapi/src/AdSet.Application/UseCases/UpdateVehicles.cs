using AdSet.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Reflection;

namespace AdSet.Application.UseCases
{
    public interface IUpdateVehicles : IUseCaseWithRequest<CreateUpdateVehicleViewModel> { }

    public class UpdateVehicles : IUpdateVehicles
    {
        private readonly IMapper mapper;
        private readonly IVehiclesRepository vehiclesRepository;
        private readonly IOptionalRepository optionalRepository;
        private readonly IVehicleOptionalRepository vehicleOptionalRepository;
        private readonly IVehicleImageRepository vehicleImageRepository;

        public UpdateVehicles(
            IMapper mapper,  
            IVehiclesRepository vehiclesRepository, 
            IOptionalRepository optionalRepository,
            IVehicleOptionalRepository vehicleOptionalRepository,
            IVehicleImageRepository vehicleImageRepository)
        {
            this.mapper = mapper;
            this.vehiclesRepository = vehiclesRepository;
            this.optionalRepository = optionalRepository;
            this.vehicleOptionalRepository = vehicleOptionalRepository;
            this.vehicleImageRepository = vehicleImageRepository;
        }

        public async Task Execute(CreateUpdateVehicleViewModel request)
        {
            var vehicleEntity = mapper.Map<Vehicle>(request);
            await vehiclesRepository.Update(vehicleEntity);

            var vehicleToUpdate = await vehiclesRepository.FindById(vehicleEntity.Id);

            if (vehicleToUpdate == null)
                throw new Exception($"Veículo com ID não encontrado.");

            mapper.Map(request, vehicleToUpdate);

            if (request.Optionals != null && request.Optionals.Any())
            {
                var optionalNames = request.Optionals;

                var optionals = await optionalRepository.FindByNames(optionalNames);

                foreach (var optional in optionals)
                {
                    var vehicleOptional = new VehicleOptional
                    {
                        VehicleId = vehicleEntity.Id,
                        OptionalId = optional.Id
                    };
                    await vehicleOptionalRepository.Add(vehicleOptional);
                }
            }

            await UpdateVehicleImages(vehicleToUpdate, request.Images, request.ExistingImageIdsToKeep);
        }

        private async Task UpdateVehicleImages(Vehicle vehicleToUpdate, IFormFileCollection newImages, List<int> existingImageIdsToKeep)
        { 
            var imagesToDelete = await vehicleImageRepository.FindByVehicleId(vehicleToUpdate.Id);

            foreach (var image in imagesToDelete)
            {
                if (!existingImageIdsToKeep.Contains(image.Id))
                {
                    var fullPath = Path.Combine(Directory.GetCurrentDirectory(), image.ImageUrl);
                    if (File.Exists(fullPath))
                    {
                        File.Delete(fullPath);
                    }
                    await vehicleImageRepository.Delete(image);
                }
            }

            if (newImages != null && newImages.Any())
            {
                var currentImageCount = await vehicleImageRepository.CountByVehicleId(vehicleToUpdate.Id);
                var remainingCapacity = 15 - currentImageCount;

                var imagesToProcess = newImages.Take(remainingCapacity);

                foreach (var imageFile in imagesToProcess)
                {
                    var uploadDirectory = "uploads/images";
                    var fullPathDir = Path.Combine(Directory.GetCurrentDirectory(), uploadDirectory);
                    if (!Directory.Exists(fullPathDir))
                    {
                        Directory.CreateDirectory(fullPathDir);
                    }

                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                    var filePath = Path.Combine(fullPathDir, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    var vehicleImage = new VehicleImage
                    {
                        VehicleId = vehicleToUpdate.Id,
                        ImageUrl = Path.Combine(uploadDirectory, fileName).Replace("\\", "/")
                    };
                    await vehicleImageRepository.Add(vehicleImage);
                }
            }
        }
    }
}
