using ADSET.DESAFIO.DOMAIN.Entities;
using ADSET.DESAFIO.DOMAIN.Interfaces;
using AutoMapper;
using MediatR;

namespace ADSET.DESAFIO.APPLICATION.Handlers.Queries
{
    public class GetCarByIdQueryHandler : IRequestHandler<GetCarByIdQuery, Car>
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public GetCarByIdQueryHandler(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        public async Task<Car> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
        {
            Car? car = await _carRepository.GetByIdAsync(request.Id);

            if (car == null)
                throw new KeyNotFoundException($"Car with id {request.Id} not found.");

            return car;
        }
    }
}
