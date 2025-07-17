using ADSET.DESAFIO.DOMAIN.Entities;
using ADSET.DESAFIO.DOMAIN.Enums;
using ADSET.DESAFIO.DOMAIN.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ADSET.DESAFIO.APPLICATION.Handlers.Commands
{
    internal class UpdateCarCommandHandler : IRequestHandler<UpdateCarCommand, Car>
    {
        public readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public UpdateCarCommandHandler(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        public async Task<Car> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
        {
            Car? car = await _carRepository.GetByIdAsync(request.Id);

            car = _mapper.Map(request.CarUpdateDto, car!);

            car.Optionals.Clear();
            if (request.CarUpdateDto.Optionals != null)
            {
                foreach (string? name in request.CarUpdateDto.Optionals)
                {
                    car.Optionals.Add(new CarOptional { Optional = new Optional(name) });

                }
            }

            car.PortalPackages.Clear();
            if (request.CarUpdateDto.PortalPackages != null)
            {
                foreach (KeyValuePair<Portal, Package> keyValue in request.CarUpdateDto.PortalPackages)
                {
                    car.PortalPackages.Add(new CarPortalPackage(request.Id, keyValue.Key, keyValue.Value));
                }
            }

            car.Photos.Clear();
            if (request.CarUpdateDto.Photos != null)
            {
                int order = 0;
                foreach (IFormFile file in request.CarUpdateDto.Photos)
                {
                    var url = $"/uploads/{file.FileName}";
                    car.Photos.Add(new CarPhoto(request.Id, url, order++));
                }
            }

            await _carRepository.UpdateAsync(car);
            return car;
        }
    }
}
