using AdSet.Domain.Entities;

namespace AdSet.Application.UseCases
{
   public interface IUpdateVehiclePortalPackages : IUseCaseWithRequest<UpdateVehiclePortalPackagesViewModel> { }

    public class UpdateVehiclePortalPackages : IUpdateVehiclePortalPackages
    {
        private readonly IVehiclePortalPackagesRepository vehiclePortalPackagesRepository;
        private readonly IPackagesRepository packagesRepository;
        private readonly IPortalRepository portalRepository;

        public UpdateVehiclePortalPackages (
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
            if (model.PortalSelections == null || !model.PortalSelections.Any())
                throw new Exception("Registry not found");

            foreach (var selection in model.PortalSelections)
            {
                await ProcessPortalPackage(model.VehicleId, selection.PortalName, selection.PackageName);
            }
        }

        private async Task ProcessPortalPackage(int vehicleId, string portalName, string? packageName)
        {
            var portal = await portalRepository.FindByName(portalName);
            if (portal == null)
                throw new Exception("Registry not found");

            if (string.IsNullOrWhiteSpace(packageName))
            {
                var existingEntry = await vehiclePortalPackagesRepository.FindByVehicleId(vehicleId, portal.Id);

                if (existingEntry != null)
                    await vehiclePortalPackagesRepository.Delete(existingEntry);
                return;
            }

            var package = await packagesRepository.FindByName(packageName);
            if (package == null)
                throw new Exception("Registry not found");

            var existingEntryForVehiclePortal = await vehiclePortalPackagesRepository.FindByVehicleId(vehicleId, portal.Id);

            if (existingEntryForVehiclePortal != null)
                existingEntryForVehiclePortal.PackageId = package.Id;
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
