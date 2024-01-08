using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using static Domain.Interfaces.IRepository;

namespace Infrastructure.Repositories
{
    public class OwnerRepository : IRepository<Owner>, IDisposable
    {
        private readonly ApplicationDbContext _context;

        public OwnerRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Owner> GetByIdAsync(Guid id)
        {
            var owner = await _context.Owners.Include(o => o.Hotels).FirstOrDefaultAsync(o => o.Id == id);

            if (owner == null)
            {
                throw new EntityNotFoundException($"Owner with ID {id} not found.");
            }

            return owner;
        }

        public IEnumerable<Owner> GetAll()
        {
            return _context.Owners.Include(o => o.Hotels);
        }

        public async Task<Owner> CreateAsync(Owner owner)
        {
            if (owner == null)
            {
                throw new ArgumentNullException(nameof(owner));
            }

            await _context.Owners.AddAsync(owner);
            await _context.SaveChangesAsync();
            return owner;
        }

        public async Task UpdateAsync(Owner owner)
        {
            if (owner == null)
            {
                throw new ArgumentNullException(nameof(owner));
            }

            var existingOwner = await _context.Owners.FirstOrDefaultAsync(o => o.Id == owner.Id);

            if (existingOwner == null)
            {
                throw new EntityNotFoundException($"Owner with ID {owner.Id} not found.");
            }

            existingOwner.FirstName = owner.FirstName;
            existingOwner.LastName = owner.LastName;
            existingOwner.Email = owner.Email;
            existingOwner.Phone = owner.Phone;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var ownerToRemove = _context.Owners.Include(o => o.Hotels).FirstOrDefault(o => o.Id == id);

            if (ownerToRemove == null)
            {
                throw new EntityNotFoundException($"Owner with ID {id} not found.");
            }

            _context.Owners.Remove(ownerToRemove);
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
