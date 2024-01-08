using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using static Domain.Interfaces.IRepository;

namespace Infrastructure.Repositories
{
    public class BookingsRepository : IRepository<Bookings>, IDisposable
    {
        private readonly ApplicationDbContext _context;
        public BookingsRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Bookings> GetByIdAsync(Guid id)
        {
            var booking = await _context.Bookings.Include(b => b.Room).Include(b => b.Guest)
                            .FirstOrDefaultAsync(booking => booking.Id == id);

            if (booking == null)
            {
                throw new EntityNotFoundException($"Booking with ID {id} not found.");
            }

            return booking;
        }

        public IEnumerable<Bookings> GetAll()
        {
            return _context.Bookings.Include(b => b.Room).Include(b => b.Guest);
        }

        public async Task<Bookings> CreateAsync(Bookings booking)
        {
            if (booking == null)
            {
                throw new ArgumentNullException(nameof(booking));
            }

            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task UpdateAsync(Bookings booking)
        {
            if (booking == null)
            {
                throw new ArgumentNullException(nameof(booking));
            }

            var existingBooking = await _context.Bookings.FirstOrDefaultAsync(b => b.Id == booking.Id);

            if (existingBooking == null)
            {
                throw new EntityNotFoundException($"Booking with ID {booking.Id} not found.");
            }


            existingBooking.RoomId = booking.RoomId;
            existingBooking.GuestId = booking.GuestId;
            existingBooking.CheckInDate = booking.CheckInDate;
            existingBooking.CheckOutDate = booking.CheckOutDate;
            existingBooking.BookingDate = booking.BookingDate;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var bookingToRemove = _context.Bookings.FirstOrDefault(user => user.Id == id);

            if (bookingToRemove == null)
            {
                throw new EntityNotFoundException($"Booking with ID {id} not found.");
            }

            _context.Bookings.Remove(bookingToRemove);
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
