using Domain.Entities;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using static Domain.Interfaces.IRepository;

namespace Infrastructure.Data.Repositories
{
    public class AppUserRepository : IRepository<AppUser>, IDisposable
    {

        private readonly ApplicationDbContext _context;
        public AppUserRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public AppUser GetById(Guid id)
        {
            var user = _context.AppUsers.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                throw new EntityNotFoundException($"User with ID {id} not found.");
            }

            return user;
        }

        public IEnumerable<AppUser> GetAll()
        {
            return _context.AppUsers;
        }

        public void Create(AppUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.AppUsers.Add(user);
            _context.SaveChanges();
        }

        public void Update(AppUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var existingUser = _context.AppUsers.FirstOrDefault(appUser => appUser.Id == user.Id);

            if (existingUser == null)
            {
                throw new EntityNotFoundException($"User with ID {user.Id} not found.");
            }

           
            existingUser.Email = user.Email;
            existingUser.PasswordHash = user.PasswordHash;
            existingUser.Role = user.Role;
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var userToRemove = _context.AppUsers.FirstOrDefault(user => user.Id == id);

            if (userToRemove != null)
            {
                _context.AppUsers.Remove(userToRemove);
                _context.SaveChanges();
            }
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
