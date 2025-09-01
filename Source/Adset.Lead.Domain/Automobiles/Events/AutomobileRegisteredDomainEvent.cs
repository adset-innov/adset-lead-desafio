using Adset.Lead.Domain.Abstractions;

namespace Adset.Lead.Domain.Automobiles.Events;

public sealed record AutomobileRegisteredDomainEvent(Guid  AutomobileId) : IDomainEvent;