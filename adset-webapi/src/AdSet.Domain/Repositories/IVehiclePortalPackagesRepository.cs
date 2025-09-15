using System.Threading.Tasks;

namespace AdSet.Domain.Repositories
{
    public interface IVehiclePortalPackagesRepository
    {
        Task<VehiclePortalPackage> Add(VehiclePortalPackage vehiclePortalPackage);
        Task<VehiclePortalPackage> FindByVehicleId(int vehicleId, int portalId);
        Task Delete(VehiclePortalPackage vehiclePortalPackage);
        Task<VehiclePortalPackage> Update(VehiclePortalPackage vehicle);
    }
}
