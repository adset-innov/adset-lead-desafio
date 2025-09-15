using AdSet.Domain.Entities;

namespace AdSet.Data.Repositories
{
    public class VehiclesRepository : IVehiclesRepository
    {
        private readonly AdSetContext context;

        public VehiclesRepository(AdSetContext context) 
            => this.context = context;


        public async Task<PagedList<Vehicle>> Search(SearchVehiclesFilter filters, int currentPage = 1, int pageSize = 10)
        {
            IQueryable<Vehicle> query = context.Vehicles
                                            .Include(v => v.VehicleImages)
                                            .Include(v => v.VehicleOptionals)
                                                .ThenInclude(vo => vo.Optional)
                                            .Include(v => v.VehiclePortalPackages)
                                                .ThenInclude(vpp => vpp.Portal) 
                                            .Include(v => v.VehiclePortalPackages)
                                                .ThenInclude(vpp => vpp.Package);

            if (!string.IsNullOrWhiteSpace(filters.Plate))
            {
                var plateFilter = filters.Plate.Trim().ToLower();
                query = query.Where(x => x.Plate.ToLower().Contains(plateFilter));
            }

            if (!string.IsNullOrWhiteSpace(filters.Make))
            {
                var makeFilter = filters.Make.Trim().ToLower();
                query = query.Where(x => x.Make.ToLower().Contains(makeFilter));
            }

            if (!string.IsNullOrWhiteSpace(filters.Model))
            {
                var modelFilter = filters.Model.Trim().ToLower();
                query = query.Where(x => x.Model.ToLower().Contains(modelFilter));
            }

            if (filters.YearMin.HasValue)
                query = query.Where(x => x.Year >= filters.YearMin.Value);

            if (filters.YearMax.HasValue)
                query = query.Where(x => x.Year <= filters.YearMax.Value);

            if (!string.IsNullOrEmpty(filters.Price))
            {
                switch (filters.Price)
                {
                    case "10-50":
                        query = query.Where(v => v.Price >= 10000 && v.Price <= 50000);
                        break;
                    case "50-90":
                        query = query.Where(v => v.Price >= 50000 && v.Price <= 90000);
                        break;
                    case "90+":
                        query = query.Where(v => v.Price > 90000);
                        break;
                }
            }

            if (filters.Images.HasValue)
            {
                if (filters.Images.Value)
                    query = query.Where(v => v.VehicleImages.Any());
                else
                    query = query.Where(v => !v.VehicleImages.Any());
            }

            if (filters.Colors != null && filters.Colors.Any())
            {
                var colorsFilter = filters.Colors
                    .Where(c => !string.IsNullOrWhiteSpace(c))
                    .Select(c => c.Trim().ToLower())
                    .ToList();

                query = query.Where(v => colorsFilter.Contains(v.Color.ToLower()));
            }

            if (filters.Optionals != null && filters.Optionals.Any())
            {
                var optionalsFilter = filters.Optionals;

                query = query.Where(v =>
                    optionalsFilter.All(optId =>
                        v.VehicleOptionals.Any(vo => vo.OptionalId == optId)));
            }

            return await query.ToPagedListAsync(currentPage, pageSize);
        }

        public async Task<Vehicle> Add(Vehicle vehicle)
        {
            context.Vehicles.Add(vehicle);
            await context.SaveChangesAsync();
            return vehicle;
        }

        public async Task<Vehicle> FindById(int vehicleId)
            =>   await context.Vehicles
               .Include(v => v.VehicleOptionals)
               .Include(v => v.VehicleImages)
               .FirstOrDefaultAsync(v => v.Id == vehicleId);


        public async Task Delete(Vehicle vehicle)
        {;
            context.Vehicles.Remove(vehicle);
            await context.SaveChangesAsync();
        }

        public async Task<Vehicle> Update(Vehicle vehicle)
        {
            context.Vehicles.Update(vehicle);
            await context.SaveChangesAsync();
            return vehicle;
        }

        public async Task<List<string>> SearchColorsDistinct()
        {
           return await context.Vehicles
                    .Select(v => v.Color)
                    .Distinct()
                    .OrderBy(c => c)
                    .ToListAsync();
        }
    }
}
