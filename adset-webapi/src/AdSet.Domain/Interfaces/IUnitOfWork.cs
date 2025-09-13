using AdSet.Domain.Repositories;

namespace AdSet.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IVehiclesRepository Vehicles { get; }
        IOptionalRepository Optionals { get; }
        IVehicleOptionalRepository VehicleOptionals { get; }
        IVehicleImageRepository VehicleImages { get; }
        IVehiclePortalPackagesRepository VehiclePortalPackages { get; }

        Task<int> CommitAsync();
    }
}