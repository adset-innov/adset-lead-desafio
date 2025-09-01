using Adset.Lead.Domain.Abstractions;
using MediatR;

namespace Adset.Lead.Application.Abstractions.CQRSCommunication;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}