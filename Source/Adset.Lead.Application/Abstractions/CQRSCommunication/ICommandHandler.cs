using Adset.Lead.Domain.Abstractions;
using MediatR;

namespace Adset.Lead.Application.Abstractions.CQRSCommunication;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand
{
}

public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>
{
}