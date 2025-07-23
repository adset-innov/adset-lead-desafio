using ADSET.DESAFIO.APPLICATION.DTOs;
using ADSET.DESAFIO.APPLICATION.Handlers.Commands;
using ADSET.DESAFIO.DOMAIN.Entities;
using ADSET.DESAFIO.DOMAIN.Enums;
using ADSET.DESAFIO.DOMAIN.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Moq;

namespace ADSET.DESAFIO.TEST.Handlers
{
    public class RegisterCarCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldCallCreateAsyncAndReturnCar()
        {
            CarCreateDTO dto = new CarCreateDTO
            {
                Brand = "Brand",
                Model = "Model",
                Year = 2022,
                Plate = "AAA1111",
                Km = 10,
                Color = "Red",
                Price = 100,
                Optionals = new List<string> { "Opt" },
                PortalPackages = new Dictionary<Portal, Package> { { Portal.iCarros, Package.Basic } }
            };

            byte[] fileBytes = new byte[] { 1, 2, 3 };
            Mock<IFormFile> fileMock = new Mock<IFormFile>();
            fileMock.Setup(f => f.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
                    .Callback<Stream, CancellationToken>((s, _) => s.Write(fileBytes, 0, fileBytes.Length))
                    .Returns(Task.CompletedTask);
            dto.Photos = new[] { fileMock.Object };

            Car car = new Car { Brand = dto.Brand, Model = dto.Model, Year = dto.Year, Plate = dto.Plate, Km = dto.Km ?? 0, Color = dto.Color, Price = dto.Price };

            Mock<IMapper> mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<Car>(dto)).Returns(car);

            Mock<ICarRepository> repoMock = new Mock<ICarRepository>();
            repoMock.Setup(r => r.CreateAsync(car)).Returns(Task.CompletedTask).Verifiable();

            RegisterCarCommandHandler handler = new RegisterCarCommandHandler(repoMock.Object, mapperMock.Object);
            RegisterCarCommand command = new RegisterCarCommand(dto);

            Car result = await handler.Handle(command, CancellationToken.None);

            repoMock.Verify(r => r.CreateAsync(car), Times.Once);
            Assert.Same(car, result);
            Assert.Single(result.Optionals);
            Assert.Single(result.PortalPackages);
            Assert.Single(result.Photos);
        }
    }
}
