namespace AdSet.Domain.Repositories
{
    public interface IPackagesRepository
    {
        Task<Package> FindByName(string name);
    }
}
