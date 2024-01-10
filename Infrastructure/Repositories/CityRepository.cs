using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using static Domain.Interfaces.IRepository;

namespace Infrastructure.Repositories
{
    public class CityRepository : IRepository<City>, IDisposable
    {
        private readonly ApplicationDbContext _context;

        public CityRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<City> GetByIdAsync(Guid id)
        {
            var city = await _context.Cities.Include(c => c.Hotels).FirstOrDefaultAsync(c => c.Id == id);

            if (city == null)
            {
                throw new EntityNotFoundException($"City with ID {id} not found.");
            }

            return city;
        }

        public IEnumerable<City> GetAll()
        {
            return _context.Cities.Include(c => c.Hotels);
        }

        public async Task<City> CreateAsync(City city)
        {
            if (city == null)
            {
                throw new ArgumentNullException(nameof(city));
            }

            _context.Cities.Add(city);
            await _context.SaveChangesAsync();
            return city;
        }

        public async Task UpdateAsync(City city)
        {
            if (city == null)
            {
                throw new ArgumentNullException(nameof(city));
            }

            var existingCity = await _context.Cities.FirstOrDefaultAsync(c => c.Id == city.Id);

            if (existingCity == null)
            {
                throw new EntityNotFoundException($"City with ID {city.Id} not found.");
            }

            existingCity.Name = city.Name;
            existingCity.CountryName = city.CountryName;
            existingCity.PostOffice = city.PostOffice;
            existingCity.CountryCode = city.CountryCode;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var cityToRemove = await _context.Cities.FirstOrDefaultAsync(c => c.Id == id);

            if (cityToRemove == null)
            {
                throw new EntityNotFoundException($"City with ID {id} not found.");
            }

            _context.Cities.Remove(cityToRemove);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
