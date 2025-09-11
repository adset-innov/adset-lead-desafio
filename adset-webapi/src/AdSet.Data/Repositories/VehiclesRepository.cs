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
            IQueryable<Vehicle> query = context.Vehicles;

            if (!string.IsNullOrEmpty(filters.Plate))
                query = query.Where(x => filters.Plate.Contains(x.Plate));

            if (!string.IsNullOrEmpty(filters.Make))
                query = query.Where(x => filters.Make.Contains(x.Make));

            if (!string.IsNullOrEmpty(filters.Model))
                query = query.Where(x => filters.Model.Contains(x.Model));

            if (filters.YearMin.HasValue)
                query = query.Where(x => x.Year >= filters.YearMin.Value);

            if (filters.YearMax.HasValue)
                query = query.Where(x => x.Year >= filters.YearMax.Value);

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

            if (filters.Colors != null && filters.Colors.Count > 0)
                query = query.Where(v => filters.Colors.Contains(v.Color));

            if (filters.Optionais != null && filters.Optionais.Count > 0)
            {
                query = query.Where(v =>
                    filters.Optionais.All(nome =>
                        v.VehicleOptionals.Any(vo => vo.Optional.Name == nome)));
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
