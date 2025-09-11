namespace AdSet.Domain.Repositories
{
    public interface IPortalRepository
    {
        Task<Portal> FindByName(string name);
    }
}
