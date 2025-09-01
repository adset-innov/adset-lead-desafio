using Adset.Lead.Application.Abstractions.CQRSCommunication;
using Adset.Lead.Domain.Automobiles;

namespace Adset.Lead.Application.Automobiles.UpdateAutomobile;

public sealed record UpdateAutomobileCommand(
    Guid AutomobileId,
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
    IReadOnlyCollection<string>? PhotoUrls = null) : ICommand;