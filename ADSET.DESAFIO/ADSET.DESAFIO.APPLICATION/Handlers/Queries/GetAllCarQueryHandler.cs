using ADSET.DESAFIO.DOMAIN.Entities;
using ADSET.DESAFIO.DOMAIN.Interfaces;
using MediatR;

namespace ADSET.DESAFIO.APPLICATION.Handlers.Queries
{
    public class GetAllCarQueryHandler : IRequestHandler<GetAllCarQuery, IEnumerable<Car>>
    {
        private readonly ICarRepository _carRepository;

        public GetAllCarQueryHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<IEnumerable<Car>> Handle(GetAllCarQuery request, CancellationToken cancellationToken)
        {
            List<Car> cars = await _carRepository.GetAllAsync();

            if (request.CarFilterDto.YearMin.HasValue)
                cars = cars.Where(c => c.Year >= request.CarFilterDto.YearMin.Value).ToList();

            if (request.CarFilterDto.YearMax.HasValue)
                cars = cars.Where(c => c.Year <= request.CarFilterDto.YearMax.Value).ToList();

            if (request.CarFilterDto.PriceMin.HasValue)
                cars = cars.Where(c => c.Price >= request.CarFilterDto.PriceMin.Value).ToList();

            if (request.CarFilterDto.PriceMax.HasValue)
                cars = cars.Where(c => c.Price <= request.CarFilterDto.PriceMax.Value).ToList();

            if (request.CarFilterDto.HasPhotos.HasValue)
                cars = request.CarFilterDto.HasPhotos.Value ? cars.Where(c => c.Photos != null && c.Photos.Any()).ToList() : cars.Where(c => c.Photos == null || !c.Photos.Any()).ToList();

            if (!string.IsNullOrWhiteSpace(request.CarFilterDto.Color))
                cars = cars.Where(c => c.Color.Equals(request.CarFilterDto.Color, StringComparison.OrdinalIgnoreCase)).ToList();

            return cars;
        }
    }
}