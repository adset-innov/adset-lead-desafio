namespace AdSet.Data.Repositories
{
    public class OptionalRepository : IOptionalRepository
    {
        private readonly AdSetContext context;

        public OptionalRepository(AdSetContext context)
            => this.context = context;

        public async Task<Optional> Add(Optional optional)
        {
            context.Optionals.Add(optional);
            await context.SaveChangesAsync();
            return optional;
        }

        public async Task<List<Optional>> FindByNames(List<string> optionalNames)
        {
            return await context.Optionals
                .Where(o => optionalNames.Contains(o.Name))
                .ToListAsync();
        }
        public async Task<List<Optional>> FindByIds(List<int> ids)
        {
            return await context.Optionals
                .Where(o => ids.Contains(o.Id))
                .ToListAsync();
        }

        public async Task<List<Optional>> GetAll()
        {
            return await context.Optionals.ToListAsync();
        }
    }
}
