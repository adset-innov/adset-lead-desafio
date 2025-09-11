namespace AdSet.Data.Repositories
{
    public class VehicleOptionalRepository : IVehicleOptionalRepository
    {
        private readonly AdSetContext context;

        public VehicleOptionalRepository(AdSetContext context)
            => this.context = context;

        public async Task<VehicleOptional> Add(VehicleOptional vehicleOptional)
        {
            context.VehicleOptionals.Add(vehicleOptional);
            await context.SaveChangesAsync();
            return vehicleOptional;
        }
    }
}
