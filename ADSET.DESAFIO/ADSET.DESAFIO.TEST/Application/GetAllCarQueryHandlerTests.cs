using ADSET.DESAFIO.APPLICATION.Common;
using ADSET.DESAFIO.APPLICATION.DTOs;
using ADSET.DESAFIO.APPLICATION.Handlers.Queries;
using ADSET.DESAFIO.DOMAIN.Entities;
using ADSET.DESAFIO.DOMAIN.Interfaces;
using Moq;

namespace ADSET.DESAFIO.TEST.Handlers
{
    public class GetAllCarQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnAllCars()
        {
            List<Car> cars = new List<Car>
            {
                new Car("B","M",2020,"AAA",0,"Red",10),
                new Car("B","X",2021,"BBB",0,"Blue",20)
            };
            Mock<ICarRepository> repoMock = new Mock<ICarRepository>();
            repoMock.Setup(r => r.QueryAll()).Returns(cars.AsQueryable()).Verifiable();
            GetAllFilterCarQueryHandler handler = new GetAllFilterCarQueryHandler(repoMock.Object);
            GetAllFilterCarQuery query = new GetAllFilterCarQuery(1, 10, new CarFilterDTO());

            PaginatedList<Car> result = await handler.Handle(query, CancellationToken.None);

            repoMock.Verify(r => r.QueryAll(), Times.Once);
            Assert.Equal(cars.Count, result.Count);
        }
    }
}