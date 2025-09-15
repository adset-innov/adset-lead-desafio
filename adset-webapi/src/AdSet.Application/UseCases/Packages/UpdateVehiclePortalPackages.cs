using AdSet.Domain.Entities;

namespace AdSet.Application.UseCases.Packages
{
    public interface IUpdateVehiclePortalPackages : IUseCaseWithRequest<UpdateVehiclePortalPackagesViewModel> { }

    public class UpdateVehiclePortalPackages : IUpdateVehiclePortalPackages
    {
        private readonly IVehiclePortalPackagesRepository vehiclePortalPackagesRepository;
        private readonly IPackagesRepository packagesRepository;
        private readonly IPortalRepository portalRepository;

        public UpdateVehiclePortalPackages(
            IVehiclePortalPackagesRepository vehiclePortalPackagesRepository,
            IPackagesRepository packagesRepository,
            IPortalRepository portalRepository)
        {
            this.vehiclePortalPackagesRepository = vehiclePortalPackagesRepository;
            this.packagesRepository = packagesRepository;
            this.portalRepository = portalRepository;
        }


        public async Task Execute(UpdateVehiclePortalPackagesViewModel model)
        {
            foreach (var selection in model.PortalSelections)
            {
                await ProcessPortalPackage(model.VehicleId, selection.PortalName, selection.PackageName);
            }
        }

        private async Task ProcessPortalPackage(int vehicleId, string portalName, string? packageName)
        {
            var portal = await portalRepository.FindByName(portalName)
                 ?? throw new Exception($"Portal '{portalName}' não encontrado.");

            var package = await packagesRepository.FindByName(packageName);


            var existingEntry = await vehiclePortalPackagesRepository.FindByVehicleId(vehicleId, portal.Id);

            if (existingEntry != null)
            {
                existingEntry.PackageId = package.Id;
                await vehiclePortalPackagesRepository.Update(existingEntry);
            }
            else
            {
                var newEntry = new VehiclePortalPackage
                {
                    VehicleId = vehicleId,
                    PortalId = portal.Id,
                    PackageId = package.Id
                };
                await vehiclePortalPackagesRepository.Add(newEntry);
            }
        }
    }
}
