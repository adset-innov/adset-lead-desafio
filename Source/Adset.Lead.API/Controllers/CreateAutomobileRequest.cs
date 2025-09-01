using Adset.Lead.Domain.Automobiles;

namespace Adset.Lead.API.Controllers;

public record CreateAutomobileRequest(
    string Brand,
    string Model,
    int Year,
    string Plate,
    string Color,
    decimal Price,
    int? Km,
    Portal Portal,
    Package Package,
    IReadOnlyCollection<OptionalFeatures>? OptionalFeatures = null,
    IReadOnlyCollection<string>? PhotoUrls = null);