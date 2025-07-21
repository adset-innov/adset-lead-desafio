using ADSET.DESAFIO.APPLICATION.Common;
using ADSET.DESAFIO.APPLICATION.DTOs;
using ADSET.DESAFIO.DOMAIN.Entities;
using MediatR;

namespace ADSET.DESAFIO.APPLICATION.Handlers.Queries
{
    public record GetAllCarQuery(int PageNumber, int PageSize, CarFilterDTO CarFilterDto) : IRequest<PaginatedList<Car>>;
}