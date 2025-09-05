using AdSet.Lead.Core.Exceptions;
using AdSet.Lead.Domain.Interfaces;

namespace AdSet.Lead.Domain.Entities;

public class Photo : IEntity
{
    public Guid Id { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public DateTime UpdatedOn { get; private set; }

    public string Url { get; private set; } = string.Empty;

    private Photo()
    {
    }

    public Photo(string url)
    {
        Validate(url);

        Id = Guid.NewGuid();
        CreatedOn = DateTime.UtcNow;
        UpdatedOn = DateTime.UtcNow;

        Url = url;
    }

    private static void Validate(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            throw new DomainValidationException("URL cannot be null or empty.");

        if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
            throw new DomainValidationException("URL is not in a valid format.");
    }
}