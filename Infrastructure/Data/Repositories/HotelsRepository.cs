using Domain.Entities;
using Domain.Exceptions;
using static Domain.Interfaces.IRepository;

namespace Infrastructure.Data.Repositories
{
    public class HotelsRepository : IRepository<Hotels>, IDisposable
    {
        private readonly ApplicationDbContext _context;

        public HotelsRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Hotels GetById(Guid id)
        {
            var hotel = _context.Hotels.FirstOrDefault(h => h.Id == id);

            if (hotel == null)
            {
                throw new EntityNotFoundException($"Hotel with ID {id} not found.");
            }

            return hotel;
        }

        public IEnumerable<Hotels> GetAll()
        {
            return _context.Hotels;
        }

        public void Create(Hotels hotel)
        {
            if (hotel == null)
            {
                throw new ArgumentNullException(nameof(hotel));
            }

            _context.Hotels.Add(hotel);
            _context.SaveChanges();
        }

        public void Update(Hotels hotel)
        {
            if (hotel == null)
            {
                throw new ArgumentNullException(nameof(hotel));
            }

            var existingHotel = _context.Hotels.FirstOrDefault(h => h.Id == hotel.Id);

            if (existingHotel == null)
            {
                throw new EntityNotFoundException($"Hotel with ID {hotel.Id} not found.");
            }

            existingHotel.CityId = hotel.CityId;
            existingHotel.OwnerId = hotel.OwnerId;
            existingHotel.Name = hotel.Name;
            existingHotel.Rating = hotel.Rating;
            existingHotel.StreetAddress = hotel.StreetAddress;
            existingHotel.Description = hotel.Description;
            existingHotel.Phone = hotel.Phone;
            existingHotel.FloorsNumber = hotel.FloorsNumber;

            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var hotelToRemove = _context.Hotels.FirstOrDefault(h => h.Id == id);

            if (hotelToRemove == null)
            {
                throw new EntityNotFoundException($"Hotel with ID {id} not found.");
            }

            _context.Hotels.Remove(hotelToRemove);
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
