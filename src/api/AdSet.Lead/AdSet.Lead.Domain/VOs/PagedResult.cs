namespace AdSet.Lead.Domain.VOs;

public sealed class PagedResult<T>(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize)
{
    public IEnumerable<T> Items { get; } = items;
    public int TotalCount { get; } = totalCount;
    public int PageNumber { get; } = pageNumber;
    public int PageSize { get; } = pageSize;
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}