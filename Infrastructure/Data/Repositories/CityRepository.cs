using Domain.Entities;
using Domain.Exceptions;
using static Domain.Interfaces.IRepository;

namespace Infrastructure.Data.Repositories
{
    public class CityRepository : IRepository<City>, IDisposable
    {
        private readonly ApplicationDbContext _context;

        public CityRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public City GetById(Guid id)
        {
            var city = _context.Cities.FirstOrDefault(c => c.Id == id);

            if (city == null)
            {
                throw new EntityNotFoundException($"City with ID {id} not found.");
            }

            return city;
        }

        public IEnumerable<City> GetAll()
        {
            return _context.Cities;
        }

        public void Create(City city)
        {
            if (city == null)
            {
                throw new ArgumentNullException(nameof(city));
            }

            _context.Cities.Add(city);
            _context.SaveChanges();
        }

        public void Update(City city)
        {
            if (city == null)
            {
                throw new ArgumentNullException(nameof(city));
            }

            var existingCity = _context.Cities.FirstOrDefault(c => c.Id == city.Id);

            if (existingCity == null)
            {
                throw new EntityNotFoundException($"City with ID {city.Id} not found.");
            }

            existingCity.Name = city.Name;
            existingCity.CountryName = city.CountryName;
            existingCity.PostOffice = city.PostOffice;
            existingCity.CountryCode = city.CountryCode;

            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var cityToRemove = _context.Cities.FirstOrDefault(c => c.Id == id);

            if (cityToRemove == null)
            {
                throw new EntityNotFoundException($"City with ID {id} not found.");
            }

            _context.Cities.Remove(cityToRemove);
            _context.SaveChanges();
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
