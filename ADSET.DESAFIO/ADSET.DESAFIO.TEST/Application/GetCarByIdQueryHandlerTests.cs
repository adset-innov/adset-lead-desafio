using ADSET.DESAFIO.APPLICATION.Handlers.Queries;
using ADSET.DESAFIO.DOMAIN.Entities;
using ADSET.DESAFIO.DOMAIN.Interfaces;
using AutoMapper;
using Moq;

namespace ADSET.DESAFIO.TEST.Application
{
    public class GetCarByIdQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnCar()
        {
            Car car = new Car("B", "M", 2020, "AAA", 0, "Red", 10) { Id = 1 };
            Mock<ICarRepository> repoMock = new Mock<ICarRepository>();
            repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(car).Verifiable();
            Mock<IMapper> mapperMock = new Mock<IMapper>();
            GetCarByIdQueryHandler handler = new GetCarByIdQueryHandler(repoMock.Object, mapperMock.Object);
            GetCarByIdQuery query = new GetCarByIdQuery(1);

            Car result = await handler.Handle(query, CancellationToken.None);

            repoMock.Verify(r => r.GetByIdAsync(1), Times.Once);
            Assert.Same(car, result);
        }
    }
}
