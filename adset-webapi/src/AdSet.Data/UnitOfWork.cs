using AdSet.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace AdSet.Infra.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AdSetContext _context;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(AdSetContext context,
                          IVehiclesRepository vehicles,
                          IOptionalRepository optionals,
                          IVehicleOptionalRepository vehicleOptionals,
                          IVehicleImageRepository vehicleImages,
                          IVehiclePortalPackagesRepository vehiclePortalPackages)
        {
            _context = context;
            Vehicles = vehicles;
            Optionals = optionals;
            VehicleOptionals = vehicleOptionals;
            VehicleImages = vehicleImages;
            VehiclePortalPackages = vehiclePortalPackages;
        }

        public IVehiclesRepository Vehicles { get; }
        public IOptionalRepository Optionals { get; }
        public IVehicleOptionalRepository VehicleOptionals { get; }
        public IVehicleImageRepository VehicleImages { get; }
        public IVehiclePortalPackagesRepository VehiclePortalPackages { get; }

        public async Task BeginTransactionAsync()
        {
            if (_transaction == null)
                _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task<int> CommitAsync()
        {
            var result = await _context.SaveChangesAsync();
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
            return result;
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}