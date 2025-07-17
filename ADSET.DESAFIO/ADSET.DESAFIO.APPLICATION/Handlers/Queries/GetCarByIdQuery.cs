using ADSET.DESAFIO.DOMAIN.Entities;
using MediatR;

namespace ADSET.DESAFIO.APPLICATION.Handlers.Queries
{
    public record GetCarByIdQuery(int Id) : IRequest<Car>;
}
