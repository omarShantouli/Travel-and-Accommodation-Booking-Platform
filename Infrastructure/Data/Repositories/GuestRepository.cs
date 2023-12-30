using Domain.Entities;
using Domain.Exceptions;
using static Domain.Interfaces.IRepository;

namespace Infrastructure.Data.Repositories
{
    public class GuestRepository : IRepository<Guest>, IDisposable
    {
        private readonly ApplicationDbContext _context;

        public GuestRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Guest GetById(Guid id)
        {
            var guest = _context.Guests.FirstOrDefault(g => g.Id == id);

            if (guest == null)
            {
                throw new EntityNotFoundException($"Guest with ID {id} not found.");
            }

            return guest;
        }

        public IEnumerable<Guest> GetAll()
        {
            return _context.Guests;
        }

        public void Create(Guest guest)
        {
            if (guest == null)
            {
                throw new ArgumentNullException(nameof(guest));
            }

            _context.Guests.Add(guest);
            _context.SaveChanges();
        }

        public void Update(Guest guest)
        {
            if (guest == null)
            {
                throw new ArgumentNullException(nameof(guest));
            }

            var existingGuest = _context.Guests.FirstOrDefault(g => g.Id == guest.Id);

            if (existingGuest == null)
            {
                throw new EntityNotFoundException($"Guest with ID {guest.Id} not found.");
            }

            existingGuest.FirstName = guest.FirstName;
            existingGuest.LastName = guest.LastName;
            existingGuest.Email = guest.Email;
            existingGuest.Phone = guest.Phone;

            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var guestToRemove = _context.Guests.FirstOrDefault(g => g.Id == id);

            if (guestToRemove == null)
            {
                throw new EntityNotFoundException($"Guest with ID {id} not found.");
            }

            _context.Guests.Remove(guestToRemove);
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
