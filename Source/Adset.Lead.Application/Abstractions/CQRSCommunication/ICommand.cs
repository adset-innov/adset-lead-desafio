using Adset.Lead.Domain.Abstractions;
using MediatR;

namespace Adset.Lead.Application.Abstractions.CQRSCommunication;

public interface ICommand : IRequest<Result>, IBaseCommand
{
}

public interface ICommand<TReponse> : IRequest<Result<TReponse>>, IBaseCommand
{
}

public interface IBaseCommand
{
}