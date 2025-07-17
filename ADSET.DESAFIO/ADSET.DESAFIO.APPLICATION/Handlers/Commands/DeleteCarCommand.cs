using MediatR;

namespace ADSET.DESAFIO.APPLICATION.Handlers.Commands
{
    public record DeleteCarCommand(int Id) : IRequest<Unit>;
}