using ADSET.DESAFIO.DOMAIN.Entities;
using ADSET.DESAFIO.DOMAIN.Interfaces;
using ADSET.DESAFIO.INFRASTRUCTURE.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ADSET.DESAFIO.INFRASTRUCTURE.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly AppDbContext _context;

        public CarRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Car car)
        {
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Car>> GetAllAsync()
        {
            return await _context.Cars.AsNoTracking()
                                      .Include(c => c.Optionals)
                                      .Include(c => c.Photos)
                                      .Include(c => c.PortalPackages)
                                      .ToListAsync();
        }

        public async Task<Car?> GetByIdAsync(int id)
        {
            return await _context.Cars.Include(c => c.Optionals)
                                      .Include(c => c.Photos)
                                      .Include(c => c.PortalPackages)
                                      .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateAsync(Car car)
        {
            _context.Cars.Update(car);
            await _context.SaveChangesAsync();
        }
    }
}