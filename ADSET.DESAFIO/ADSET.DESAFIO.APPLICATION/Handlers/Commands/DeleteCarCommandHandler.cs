using ADSET.DESAFIO.DOMAIN.Entities;
using ADSET.DESAFIO.DOMAIN.Interfaces;
using MediatR;

namespace ADSET.DESAFIO.APPLICATION.Handlers.Commands
{
    public class DeleteCarCommandHandler : IRequestHandler<DeleteCarCommand, Unit>
    {
        private readonly ICarRepository _carRepository;

        public DeleteCarCommandHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<Unit> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
        {
            Car? car = await _carRepository.GetByIdAsync(request.Id) ?? null;

            if (car == null)
                throw new KeyNotFoundException($"Car: {request.Id} not found!");

            await _carRepository.DeleteAsync(car);

            return Unit.Value;
        }
    }
}