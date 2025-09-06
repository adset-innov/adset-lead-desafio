using AdSet.Lead.Core.Exceptions;
using AdSet.Lead.Domain.Interfaces;

namespace AdSet.Lead.Domain.Filters;

public sealed class PaginationFilter : IPaginationFilter
{
    public int PageNumber { get; }
    public int PageSize { get; }

    private PaginationFilter()
    {
    }

    public PaginationFilter(int pageNumber, int pageSize)
    {
        if (pageNumber <= 0)
            throw new DomainValidationException("Page number must be greater than 0.");

        if (pageSize is <= 0 or > 100)
            throw new DomainValidationException("Page size must be between 1 and 100.");

        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}