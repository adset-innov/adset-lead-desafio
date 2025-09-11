using Microsoft.EntityFrameworkCore;

namespace AdSet.Data.Repositories
{
    public class VehicleImageRepository : IVehicleImageRepository
    {
        private readonly AdSetContext context;

        public VehicleImageRepository(AdSetContext context)
            => this.context = context;

        public async Task<VehicleImage> Add(VehicleImage image)
        {
            context.VehicleImages.Add(image);
            await context.SaveChangesAsync();
            return image;
        }

        public async Task Delete(VehicleImage image)
        {
            context.VehicleImages.Remove(image);
            await context.SaveChangesAsync();
        }

        public async Task<List<VehicleImage>> FindByVehicleId(int vehicleId)
        {
            return await context.VehicleImages
                                 .Where(vi => vi.VehicleId == vehicleId)
                                 .ToListAsync();
        }

        public async Task<int> CountByVehicleId(int vehicleId)
        {
            return await context.VehicleImages
                                 .CountAsync(vi => vi.VehicleId == vehicleId);
        }
    }
}
