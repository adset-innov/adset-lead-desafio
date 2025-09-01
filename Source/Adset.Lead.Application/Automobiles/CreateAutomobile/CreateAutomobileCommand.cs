using Adset.Lead.Application.Abstractions.CQRSCommunication;
using Adset.Lead.Domain.Automobiles;

namespace Adset.Lead.Application.Automobiles.CreateAutomobile;

public sealed record CreateAutomobileCommand(
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
    IReadOnlyCollection<string>? FileNames = null) : ICommand<Guid>;