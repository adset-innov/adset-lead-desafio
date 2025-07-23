using ADSET.DESAFIO.APPLICATION.DTOs;
using ADSET.DESAFIO.APPLICATION.Handlers.Commands;
using ADSET.DESAFIO.DOMAIN.Entities;
using ADSET.DESAFIO.DOMAIN.Enums;
using ADSET.DESAFIO.DOMAIN.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Moq;

namespace ADSET.DESAFIO.TEST.Application
{
    public class UpdateCarCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldCallUpdateAsyncAndReturnCar()
        {
            CarUpdateDTO dto = new CarUpdateDTO
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

            Car existingCar = new Car { Id = 1 };

            Mock<IMapper> mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map(dto, existingCar)).Returns(existingCar);

            Mock<ICarRepository> repoMock = new Mock<ICarRepository>();
            repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existingCar);
            repoMock.Setup(r => r.UpdateAsync(existingCar)).Returns(Task.CompletedTask).Verifiable();

            UpdateCarCommandHandler handler = new UpdateCarCommandHandler(repoMock.Object, mapperMock.Object);
            UpdateCarCommand command = new UpdateCarCommand(1, dto);

            Car result = await handler.Handle(command, CancellationToken.None);

            repoMock.Verify(r => r.UpdateAsync(existingCar), Times.Once);
            Assert.Same(existingCar, result);
            Assert.Single(result.Optionals);
            Assert.Single(result.PortalPackages);
            Assert.Single(result.Photos);
        }
    }
}
