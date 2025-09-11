namespace AdSet.Domain.Repositories
{
    public interface IVehicleImageRepository
    {
        Task<VehicleImage> Add(VehicleImage image);
        Task Delete(VehicleImage image);
        Task<List<VehicleImage>> FindByVehicleId(int vehicleId);
        Task<int> CountByVehicleId(int vehicleId);
    }
}
