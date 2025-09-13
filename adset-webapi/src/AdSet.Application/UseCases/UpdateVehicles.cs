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
            vehicleEntity.Id  = request.Id;
            //lembrar de usar UnitOfWork
            await vehiclesRepository.Update(vehicleEntity);

            var vehicleToUpdate = await vehiclesRepository.FindById(vehicleEntity.Id);

            if (vehicleToUpdate == null)
                throw new Exception($"Veículo com ID não encontrado.");

            mapper.Map(request, vehicleToUpdate);

            if (request.Optionals != null && request.Optionals.Any())
            {
                var optionalIds = request.Optionals;
                var optionals = await optionalRepository.FindByIds(optionalIds);

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

            await UpdateVehicleImages(vehicleToUpdate, request.Images);
        }

        private async Task UpdateVehicleImages(Vehicle vehicleToUpdate, IFormFileCollection newImages)
        {
            var imagesToDelete = await vehicleImageRepository.FindByVehicleId(vehicleToUpdate.Id);

            if (newImages != null && newImages.Any())
            {
                var currentImageCount = await vehicleImageRepository.CountByVehicleId(vehicleToUpdate.Id);
                var remainingCapacity = 15 - currentImageCount;

                var imagesToProcess = newImages.Take(remainingCapacity);

                string solutionRoot = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
                string vehicleFolder = Path.Combine(solutionRoot, "uploads", "images", vehicleToUpdate.Id.ToString());

                if (!Directory.Exists(vehicleFolder))
                {
                    Directory.CreateDirectory(vehicleFolder);
                }

                foreach (var imageFile in imagesToProcess)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                    var filePath = Path.Combine(vehicleFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    var vehicleImage = new VehicleImage
                    {
                        VehicleId = vehicleToUpdate.Id,
                        // 🔹 URL pública relativa, que o Angular vai consumir
                        ImageUrl = $"/uploads/images/{vehicleToUpdate.Id}/{fileName}"
                    };

                    await vehicleImageRepository.Add(vehicleImage);
                }
            }
        }
    }
}
