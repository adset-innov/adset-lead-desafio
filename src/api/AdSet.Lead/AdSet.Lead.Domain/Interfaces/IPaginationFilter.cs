namespace AdSet.Lead.Domain.Interfaces;

public interface IPaginationFilter
{
    int PageNumber { get; }
    int PageSize { get; }
}