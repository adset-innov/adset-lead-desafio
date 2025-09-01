using Adset.Lead.Application.Abstractions.CQRSCommunication;

namespace Adset.Lead.Application.Automobiles.SearchAutomobile;

public sealed record SearchAutomobileQuery(
    Guid? Id = null,
    string? Plate = null,
    string? Brand = null,
    string? Model = null,
    int? YearMin = null,
    int? YearMax = null,
    decimal? PriceMin = null,
    decimal? PriceMax = null,
    string? Color = null,
    bool? HasPhotos = null,
    int? Portal = null,
    int? Package = null,
    int? Feature = null
) : IQuery<IReadOnlyList<SearchAutomobileResponse>>;