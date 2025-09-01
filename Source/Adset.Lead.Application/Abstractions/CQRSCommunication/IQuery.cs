using Adset.Lead.Domain.Abstractions;
using MediatR;

namespace Adset.Lead.Application.Abstractions.CQRSCommunication;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}