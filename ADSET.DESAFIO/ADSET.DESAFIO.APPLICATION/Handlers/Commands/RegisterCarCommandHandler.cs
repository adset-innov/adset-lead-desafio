using ADSET.DESAFIO.DOMAIN.Entities;
using ADSET.DESAFIO.DOMAIN.Enums;
using ADSET.DESAFIO.DOMAIN.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ADSET.DESAFIO.APPLICATION.Handlers.Commands
{
    public class RegisterCarCommandHandler : IRequestHandler<RegisterCarCommand, Car>
    {
        public readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;
        public RegisterCarCommandHandler(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        public async Task<Car> Handle(RegisterCarCommand request, CancellationToken cancellationToken)
        {
            Car car = _mapper.Map<Car>(request.carCreateDto);

            if (request.carCreateDto.Optionals != null)
            {
                foreach (string name in request.carCreateDto.Optionals)
                {
                    car.Optionals.Add(new CarOptional { Optional = new Optional(name) });
                }
            }

            if (request.carCreateDto.PortalPackages != null)
            {
                foreach (KeyValuePair<Portal, Package> keyValue in request.carCreateDto.PortalPackages)
                {
                    car.PortalPackages.Add(new CarPortalPackage(0, keyValue.Key, keyValue.Value));
                }
            }

            if (request.carCreateDto.Photos != null)
            {
                int order = 0;

                foreach (IFormFile IFormFile in request.carCreateDto.Photos)
                {
                    string? url = $"/uploads/{IFormFile.FileName}";
                    car.Photos.Add(new CarPhoto(0, url, order++));
                }
            }

            await _carRepository.CreateAsync(car);

            return car;
        }
    }
}
