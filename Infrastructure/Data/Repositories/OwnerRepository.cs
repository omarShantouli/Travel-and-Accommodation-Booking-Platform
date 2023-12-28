using Domain.Entities;
using Domain.Exceptions;
using static Domain.Interfaces.IRepository;

namespace Infrastructure.Data.Repositories
{
    public class OwnerRepository : IRepository<Owner>, IDisposable
    {
        private readonly ApplicationDbContext _context;

        public OwnerRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Owner GetById(Guid id)
        {
            var owner = _context.Owners.FirstOrDefault(o => o.Id == id);

            if (owner == null)
            {
                throw new EntityNotFoundException($"Owner with ID {id} not found.");
            }

            return owner;
        }

        public IEnumerable<Owner> GetAll()
        {
            return _context.Owners;
        }

        public void Create(Owner owner)
        {
            if (owner == null)
            {
                throw new ArgumentNullException(nameof(owner));
            }

            _context.Owners.Add(owner);
            _context.SaveChanges();
        }

        public void Update(Owner owner)
        {
            if (owner == null)
            {
                throw new ArgumentNullException(nameof(owner));
            }

            var existingOwner = _context.Owners.FirstOrDefault(o => o.Id == owner.Id);

            if (existingOwner == null)
            {
                throw new EntityNotFoundException($"Owner with ID {owner.Id} not found.");
            }

            existingOwner.FirstName = owner.FirstName;
            existingOwner.LastName = owner.LastName;
            existingOwner.Email = owner.Email;
            existingOwner.Phone = owner.Phone;

            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var ownerToRemove = _context.Owners.FirstOrDefault(o => o.Id == id);

            if (ownerToRemove == null)
            {
                throw new EntityNotFoundException($"Owner with ID {id} not found.");
            }

            _context.Owners.Remove(ownerToRemove);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
