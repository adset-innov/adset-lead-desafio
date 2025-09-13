using AdSet.Domain.Interfaces;

namespace AdSet.Application.UseCases
{
    public interface ICreateVehicles : IUseCaseWithRequest<CreateUpdateVehicleViewModel> { }

    public class CreateVehicles : ICreateVehicles
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IVehiclesRepository vehiclesRepository;
        private readonly IOptionalRepository optionalRepository;
        private readonly IVehicleOptionalRepository vehicleOptionalRepository;
        private readonly IVehicleImageRepository vehicleImageRepository;

        public CreateVehicles(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IVehiclesRepository vehiclesRepository,
            IOptionalRepository optionalRepository,
            IVehicleOptionalRepository vehicleOptionalRepository,
            IVehicleImageRepository vehicleImageRepository) 
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.vehiclesRepository = vehiclesRepository;
            this.optionalRepository = optionalRepository;
            this.vehicleOptionalRepository = vehicleOptionalRepository;
            this.vehicleImageRepository = vehicleImageRepository;
        }

        public async Task Execute(CreateUpdateVehicleViewModel request)
        {
            
            var vehicleEntity = mapper.Map<Vehicle>(request);
            //usar o UnitOfWork
            await vehiclesRepository.Add(vehicleEntity);

            await ProcessOptionals(request, vehicleEntity.Id);
            await ProcessImages(request, vehicleEntity.Id);

        }

        private async Task ProcessOptionals(CreateUpdateVehicleViewModel request, int vehicleEntityId)
        {
            if (request.Optionals != null && request.Optionals.Any())
            {
                var optionalIds = request.Optionals;

                var optionals = await optionalRepository.FindByIds(optionalIds);

                foreach (var optional in optionals)
                {
                    var vehicleOptional = new VehicleOptional
                    {
                        VehicleId = vehicleEntityId,
                        OptionalId = optional.Id
                    };
                    await vehicleOptionalRepository.Add(vehicleOptional);
                }
            }
        }

        private async Task ProcessImages(CreateUpdateVehicleViewModel request, int vehicleEntityId)
        {
            if (request.Images != null && request.Images.Any())
            {
                var imagesToProcess = request.Images.Take(15);
         
                string fullPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
                string uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "uploads", "images", vehicleEntityId.ToString());

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

                    var relativePath = $"/uploads/images/{vehicleEntityId}/{fileName}";
                    var vehicleImage = new VehicleImage
                    {
                        VehicleId = vehicleEntityId,
                        ImageUrl = relativePath
                    };
                    await vehicleImageRepository.Add(vehicleImage);
                }
            }
        }
    }
}
