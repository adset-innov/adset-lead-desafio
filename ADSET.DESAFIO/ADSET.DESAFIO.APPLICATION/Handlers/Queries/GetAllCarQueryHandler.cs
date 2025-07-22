using ADSET.DESAFIO.APPLICATION.Common;
using ADSET.DESAFIO.DOMAIN.Entities;
using ADSET.DESAFIO.DOMAIN.Interfaces;
using MediatR;

namespace ADSET.DESAFIO.APPLICATION.Handlers.Queries
{
    public class GetAllCarQueryHandler : IRequestHandler<GetAllCarQuery, PaginatedList<Car>>
    {
        private readonly ICarRepository _carRepository;

        public GetAllCarQueryHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<PaginatedList<Car>> Handle(GetAllCarQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Car> cars = _carRepository.QueryAll();

            if (!string.IsNullOrEmpty(request.CarFilterDto.Brand))
                cars = cars.Where(c => c.Brand.Contains(request.CarFilterDto.Brand));

            if (!string.IsNullOrEmpty(request.CarFilterDto.Model))
                cars = cars.Where(c => c.Model.Contains(request.CarFilterDto.Model));

            if (!string.IsNullOrEmpty(request.CarFilterDto.Plate))
                cars = cars.Where(c => c.Plate.Contains(request.CarFilterDto.Plate));

            if (request.CarFilterDto.YearMin.HasValue)
                cars = cars.Where(c => c.Year >= request.CarFilterDto.YearMin.Value);

            if (request.CarFilterDto.YearMax.HasValue)
                cars = cars.Where(c => c.Year <= request.CarFilterDto.YearMax.Value);

            if (request.CarFilterDto.PriceMin.HasValue && request.CarFilterDto.PriceMin != 0)
                cars = cars.Where(c => c.Price >= request.CarFilterDto.PriceMin.Value);

            if (request.CarFilterDto.PriceMax.HasValue && request.CarFilterDto.PriceMax != 0)
                cars = cars.Where(c => c.Price <= request.CarFilterDto.PriceMax.Value);

            if (request.CarFilterDto.HasPhotos.HasValue)
                cars = request.CarFilterDto.HasPhotos.Value ? cars.Where(c => c.Photos != null && c.Photos.Any()) : cars.Where(c => c.Photos == null || !c.Photos.Any());

            if (!string.IsNullOrWhiteSpace(request.CarFilterDto.Color))
                cars = cars.Where(c => c.Color.Equals(request.CarFilterDto.Color, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(request.CarFilterDto.SortBy))
            {
                bool desc = string.Equals(request.CarFilterDto.SortDir, "desc", StringComparison.OrdinalIgnoreCase);
                cars = request.CarFilterDto.SortBy.ToLower() switch
                {
                    "brand" => desc ? cars.OrderByDescending(c => c.Brand) : cars.OrderBy(c => c.Brand),
                    "model" => desc ? cars.OrderByDescending(c => c.Model) : cars.OrderBy(c => c.Model),
                    "year" => desc ? cars.OrderByDescending(c => c.Year) : cars.OrderBy(c => c.Year),
                    "price" => desc ? cars.OrderByDescending(c => c.Price) : cars.OrderBy(c => c.Price),
                    "photos" => desc ? cars.OrderByDescending(c => c.Photos.Count) : cars.OrderBy(c => c.Photos.Count),
                    _ => cars
                };
            }

            return await Task.Run(() => PaginatedList<Car>.CreateAsync(cars, request.PageNumber, request.PageSize));
        }
    }
}