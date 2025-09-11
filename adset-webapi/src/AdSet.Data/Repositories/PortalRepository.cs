namespace AdSet.Data.Repositories
{
    public class PortalRepository : IPortalRepository
    {
        private readonly AdSetContext context;

        public PortalRepository(AdSetContext context)
        => this.context = context;

        public async Task<Portal> FindByName(string name)
        {
           return await context.Portals.FirstOrDefaultAsync(p => p.Name == name);
        }
    }
}
