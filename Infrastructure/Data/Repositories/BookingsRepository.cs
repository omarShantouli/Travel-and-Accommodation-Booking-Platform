using Domain.Entities;
using Domain.Exceptions;
using static Domain.Interfaces.IRepository;

namespace Infrastructure.Data.Repositories
{
    public class BookingsRepository : IRepository<Bookings>, IDisposable
    {
        private readonly ApplicationDbContext _context;
        public BookingsRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Bookings GetById(Guid id)
        {
            var booking = _context.Bookings.FirstOrDefault(booking => booking.Id == id);

            if (booking == null)
            {
                throw new EntityNotFoundException($"Booking with ID {id} not found.");
            }

            return booking;
        }

        public IEnumerable<Bookings> GetAll()
        {
            return _context.Bookings;
        }

        public void Create(Bookings booking)
        {
            if (booking == null)
            {
                throw new ArgumentNullException(nameof(booking));
            }

            _context.Bookings.Add(booking);
            _context.SaveChanges();
        }

        public void Update(Bookings booking)
        {
            if (booking == null)
            {
                throw new ArgumentNullException(nameof(booking));
            }

            var existingBooking = _context.Bookings.FirstOrDefault(b => b.Id == booking.Id);

            if (existingBooking == null)
            {
                throw new EntityNotFoundException($"Booking with ID {booking.Id} not found.");
            }


            existingBooking.RoomId = booking.RoomId;
            existingBooking.GuestId = booking.GuestId;
            existingBooking.CheckInDate = booking.CheckInDate;
            existingBooking.CheckOutDate = booking.CheckOutDate;
            existingBooking.BookingDate = booking.BookingDate;

            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var bookingToRemove = _context.Bookings.FirstOrDefault(user => user.Id == id);

            if (bookingToRemove == null)
            {
                throw new EntityNotFoundException($"Booking with ID {id} not found.");
            }

            _context.Bookings.Remove(bookingToRemove);
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
