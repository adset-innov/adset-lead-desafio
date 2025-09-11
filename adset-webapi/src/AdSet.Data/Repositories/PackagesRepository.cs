using AdSet.Domain.Entities;

namespace AdSet.Data.Repositories
{
    public class PackagesRepository : IPackagesRepository
    {
        private readonly AdSetContext context;

        public PackagesRepository(AdSetContext context)
            => this.context = context;


        public async Task<Package> FindByName(string name)
        {
            return await context.Packages.FirstOrDefaultAsync(p => p.Name == name);
        }

    }
}
