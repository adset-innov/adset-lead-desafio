using Adset.Lead.Application.Abstractions.CQRSCommunication;

namespace Adset.Lead.Application.Automobiles.DeleteAutomobile;

public record DeleteAutomobileCommand(Guid AutomobileId) : ICommand;
