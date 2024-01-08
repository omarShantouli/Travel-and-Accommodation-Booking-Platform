using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using static Domain.Interfaces.IRepository;

namespace Infrastructure.Repositories
{
    public class GuestRepository : IRepository<Guest>, IDisposable
    {
        private readonly ApplicationDbContext _context;

        public GuestRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Guest> GetByIdAsync(Guid id)
        {
            var guest = await _context.Guests.Include(g => g.Bookings).FirstOrDefaultAsync(g => g.Id == id);

            if (guest == null)
            {
                throw new EntityNotFoundException($"Guest with ID {id} not found.");
            }

            return guest;
        }

        public IEnumerable<Guest> GetAll()
        {
            return _context.Guests.Include(g => g.Bookings);
        }

        public async Task<Guest> CreateAsync(Guest guest)
        {
            if (guest == null)
            {
                throw new ArgumentNullException(nameof(guest));
            }

            await _context.Guests.AddAsync(guest);
            await _context.SaveChangesAsync();
            return guest;
        }

        public async Task UpdateAsync(Guest guest)
        {
            if (guest == null)
            {
                throw new ArgumentNullException(nameof(guest));
            }

            var existingGuest = await _context.Guests.FirstOrDefaultAsync(g => g.Id == guest.Id);

            if (existingGuest == null)
            {
                throw new EntityNotFoundException($"Guest with ID {guest.Id} not found.");
            }

            existingGuest.FirstName = guest.FirstName;
            existingGuest.LastName = guest.LastName;
            existingGuest.Email = guest.Email;
            existingGuest.Phone = guest.Phone;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var guestToRemove = _context.Guests.FirstOrDefault(g => g.Id == id);

            if (guestToRemove == null)
            {
                throw new EntityNotFoundException($"Guest with ID {id} not found.");
            }

            _context.Guests.Remove(guestToRemove);
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
