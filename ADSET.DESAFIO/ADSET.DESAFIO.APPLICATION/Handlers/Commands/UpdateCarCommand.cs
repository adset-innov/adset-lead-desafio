using ADSET.DESAFIO.APPLICATION.DTOs;
using ADSET.DESAFIO.DOMAIN.Entities;
using MediatR;

namespace ADSET.DESAFIO.APPLICATION.Handlers.Commands
{
    public record UpdateCarCommand(int Id, CarUpdateDTO CarUpdateDto) : IRequest<Car>;
}
