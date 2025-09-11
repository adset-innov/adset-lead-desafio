using AdSet.Domain.Entities;

namespace AdSet.Data.Repositories
{
    public class VehiclePortalPackagesRepository : IVehiclePortalPackagesRepository
    {
        private readonly AdSetContext context;

        public VehiclePortalPackagesRepository(AdSetContext context)
            => this.context = context;

        public async Task<VehiclePortalPackage> Add(VehiclePortalPackage vehiclePortalPackage)
        {
            context.VehiclePortalPackages.Add(vehiclePortalPackage);
            await context.SaveChangesAsync();
            return vehiclePortalPackage;
        }

        public async Task<VehiclePortalPackage> FindByVehicleId(int vehicleId, int portalId)
        {
            return await context.VehiclePortalPackages
               .FirstOrDefaultAsync(vpp => vpp.VehicleId == vehicleId && vpp.PortalId == portalId);
        }

        public async Task Delete(VehiclePortalPackage vehiclePortalPackage)
        {
            context.VehiclePortalPackages.Remove(vehiclePortalPackage);
            await context.SaveChangesAsync();
        }
         
    }
}
