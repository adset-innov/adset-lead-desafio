using ADSET.DESAFIO.DOMAIN.Entities;
using ADSET.DESAFIO.DOMAIN.Interfaces;
using MediatR;

namespace ADSET.DESAFIO.APPLICATION.Handlers.Queries
{
    public class GetAllCarQueryHandler : IRequestHandler<GetAllCarQuery, List<Car>>
    {
        private readonly ICarRepository _carRepository;

        public GetAllCarQueryHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<List<Car>> Handle(GetAllCarQuery request, CancellationToken cancellationToken)
        {
            return await Task.Run(() => _carRepository.QueryAll().ToList());
        }
    }
}