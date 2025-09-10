namespace AdSet.Data
{
    public static class PageableAsyncExtensions
    {
        public static async Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source, int currentPage, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
            return new(items, count, currentPage, pageSize);
        }
    }
}
