using ADSET.DESAFIO.APPLICATION.DTOs;
using ADSET.DESAFIO.DOMAIN.Entities;
using MediatR;

namespace ADSET.DESAFIO.APPLICATION.Handlers.Queries
{
    public record GetAllCarQuery(CarFilterDTO CarFilterDto) : IRequest<IEnumerable<Car>>;
}