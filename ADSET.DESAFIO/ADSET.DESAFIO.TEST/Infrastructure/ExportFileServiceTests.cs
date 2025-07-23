using ADSET.DESAFIO.DOMAIN.Entities;
using ADSET.DESAFIO.DOMAIN.Interfaces;
using ADSET.DESAFIO.INFRASTRUCTURE.Services;
using Moq;

namespace ADSET.DESAFIO.TEST.Infrastructure
{
    public class ExportFileServiceTests
    {
        [Fact]
        public async Task ExportToExcelAsync_ShouldReturnBytes()
        {
            List<Car> cars = new List<Car> { new Car("B", "M", 2020, "AAA", 0, "Red", 10) { Id = 1 } };
            Mock<ICarRepository> repoMock = new Mock<ICarRepository>();
            repoMock.Setup(r => r.QueryAll()).Returns(cars.AsQueryable()).Verifiable();

            ExportFileService service = new ExportFileService(repoMock.Object);

            byte[] result = await service.ExportToExcelAsync();

            repoMock.Verify(r => r.QueryAll(), Times.Once);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
    }
}
