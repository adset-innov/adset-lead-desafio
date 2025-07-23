using ADSET.DESAFIO.DOMAIN.Entities;
using ADSET.DESAFIO.DOMAIN.Interfaces;

namespace ADSET.DESAFIO.TEST.FakeRepository
{
    public class FakeCarRepository : ICarRepository
    {
        private readonly List<Car> _cars;

        public FakeCarRepository(List<Car> initial = null!)
        {
            _cars = initial ?? new List<Car>();
        }

        public Task<Car?> GetByIdAsync(int id)
        {
            Car? car = _cars.FirstOrDefault(c => c.Id == id);
            return Task.FromResult(car);
        }

        public Task DeleteAsync(Car car)
        {
            _cars.Remove(car);
            return Task.CompletedTask;
        }

        public Task<int> SaveChangesAsync()
        {
            return Task.FromResult(1);
        }

        public Task CreateAsync(Car car)
        {
            if (car.Id == 0)
                car.Id = _cars.Count > 0 ? _cars.Max(c => c.Id) + 1 : 1;

            _cars.Add(car);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Car car)
        {
            int idx = _cars.FindIndex(c => c.Id == car.Id);
            if (idx >= 0) _cars[idx] = car;
            return Task.CompletedTask;
        }

        public IQueryable<Car> QueryAll() => _cars.AsQueryable();

        public IReadOnlyList<Car> Cars => _cars;
    }
}
