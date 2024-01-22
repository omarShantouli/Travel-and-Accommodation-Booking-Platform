using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using static Domain.Interfaces.IRepository;

namespace Infrastructure.Repositories
{
    public class HotelsRepository : IRepository<Hotels>, IDisposable
    {
        private readonly ApplicationDbContext _context;

        public HotelsRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Hotels> GetByIdAsync(Guid id)
        {
            var hotel = await _context.Hotels.Include(h => h.Rooms).ThenInclude(r => r.Bookings).Include(h => h.Owner).Include(h => h.City)
                .FirstOrDefaultAsync(h => h.Id == id);

            if (hotel == null)
            {
                throw new EntityNotFoundException($"Hotel with ID {id} not found.");
            }

            return hotel;
        }

        public IEnumerable<Hotels> GetAll()
        {
            return _context.Hotels.Include(h => h.Rooms).ThenInclude(r => r.Bookings).Include(h => h.Owner).Include(h => h.City);
        }

        public async Task<Hotels> CreateAsync(Hotels hotel)
        {
            if (hotel == null)
            {
                throw new ArgumentNullException(nameof(hotel));
            }

            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();
            return hotel;
        }

        public async Task UpdateAsync(Hotels hotel)
        {
            if (hotel == null)
            {
                throw new ArgumentNullException(nameof(hotel));
            }

            var existingHotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == hotel.Id);

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

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var hotelToRemove = _context.Hotels.FirstOrDefault(h => h.Id == id);

            if (hotelToRemove == null)
            {
                throw new EntityNotFoundException($"Hotel with ID {id} not found.");
            }

            _context.Hotels.Remove(hotelToRemove);
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
