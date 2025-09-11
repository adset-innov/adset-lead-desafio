using AdSet.Domain.Entities;
using AutoMapper;

namespace AdSet.Application.UseCases
{
    public interface ICreateVehicles : IUseCaseWithRequest<CreateUpdateVehicleViewModel> { }

    public class CreateVehicles : ICreateVehicles
    {
        private readonly IMapper mapper;
        private readonly IVehiclesRepository vehiclesRepository;
        private readonly IOptionalRepository optionalRepository;
        private readonly IVehicleOptionalRepository vehicleOptionalRepository;
        private readonly IVehicleImageRepository vehicleImageRepository;

        public CreateVehicles(
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
            await vehiclesRepository.Add(vehicleEntity);

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

            if (request.Images != null && request.Images.Any())
            {
                var imagesToProcess = request.Images.Take(15);
                string uploadDirectory = "uploads/images";
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), uploadDirectory);

                if (!Directory.Exists(fullPath))
                {
                    Directory.CreateDirectory(fullPath);
                }

                foreach (var imageFile in imagesToProcess)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                    var filePath = Path.Combine(fullPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    var vehicleImage = new VehicleImage
                    {
                        VehicleId = vehicleEntity.Id,
                        ImageUrl = Path.Combine(uploadDirectory, fileName).Replace("\\", "/")
                    };
                    await vehicleImageRepository.Add(vehicleImage);
                }
            }
        }
    }
}
