using ADSET.DESAFIO.APPLICATION.Handlers.Commands;
using ADSET.DESAFIO.DOMAIN.Entities;
using ADSET.DESAFIO.TEST.FakeRepository;

namespace ADSET.DESAFIO.TEST.Handlers
{
    public class DeleteCarCommandHandlerTests
    {
        [Fact]
        public async Task Handle_RemovesCar_FromInMemoryList()
        {
            Car existing = new Car("Ford", "Fiesta", 2020, "ABC-1234", 0, "Red", 10000m)
            {
                Id = 42
            };
            FakeCarRepository fakeRepo = new FakeCarRepository(new List<Car> { existing });
            DeleteCarCommandHandler handler = new DeleteCarCommandHandler(fakeRepo);

            await handler.Handle(new DeleteCarCommand(42), CancellationToken.None);

            Assert.Empty(fakeRepo.Cars);
        }
    }
}