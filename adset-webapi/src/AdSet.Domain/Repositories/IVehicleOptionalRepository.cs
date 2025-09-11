namespace AdSet.Domain.Repositories
{
    public interface IVehicleOptionalRepository
    {
        Task<VehicleOptional> Add(VehicleOptional optional);
    }
}
