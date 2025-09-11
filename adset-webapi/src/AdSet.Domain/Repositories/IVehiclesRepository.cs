namespace AdSet.Domain.Repositories
{
    public interface IVehiclesRepository
    {
        Task<PagedList<Vehicle>> Search(SearchVehiclesFilter filters, int currentPage = 1, int pageSize = 10);
        Task<Vehicle> FindById(int vehicleId);
        Task<Vehicle> Add(Vehicle vechicle);
        Task<Vehicle>Update(Vehicle vehicle);
        Task Delete(Vehicle vechicle);
        Task<List<string>> SearchColorsDistinct();
    }
}
