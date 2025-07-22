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
            Car car = _mapper.Map<Car>(request.CarCreateDto);

            if (request.CarCreateDto.Optionals != null)
            {
                foreach (string name in request.CarCreateDto.Optionals)
                {
                    car.Optionals.Add(new CarOptional { Optional = new Optional(name) });
                }
            }

            if (request.CarCreateDto.PortalPackages != null)
            {
                foreach (KeyValuePair<Portal, Package> keyValue in request.CarCreateDto.PortalPackages)
                {
                    car.PortalPackages.Add(new CarPortalPackage(0, keyValue.Key, keyValue.Value));
                }
            }

            if (request.CarCreateDto.Photos != null)
            {
                int order = 0;

                foreach (IFormFile file in request.CarCreateDto.Photos)
                {
                    using MemoryStream stream = new MemoryStream();
                    await file.CopyToAsync(stream);
                    byte[] bytes = stream.ToArray();

                    car.Photos.Add(new CarPhoto
                    {
                        CarId = 0,
                        PhotoData = bytes,
                        Order = order++
                    });
                }
            }

            await _carRepository.CreateAsync(car);

            return car;
        }
    }
}
