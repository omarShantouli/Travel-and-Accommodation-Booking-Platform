using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using static Domain.Interfaces.IRepository;

namespace Infrastructure.Repositories
{
    public class AppUserRepository : IRepository<AppUser>, IDisposable
    {

        private readonly ApplicationDbContext _context;
        public AppUserRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<AppUser> GetByIdAsync(Guid id)
        {
            var user = await _context.AppUsers.FirstOrDefaultAsync(u => u.Id == id);

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

        public async Task<AppUser> CreateAsync(AppUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

             _context.AppUsers.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task UpdateAsync(AppUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var existingUser = await _context.AppUsers.FirstOrDefaultAsync(appUser => appUser.Id == user.Id);

            if (existingUser == null)
            {
                throw new EntityNotFoundException($"User with ID {user.Id} not found.");
            }


            existingUser.Email = user.Email;
            existingUser.PasswordHash = user.PasswordHash;
            existingUser.Role = user.Role;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var userToRemove = await _context.AppUsers.FirstOrDefaultAsync(user => user.Id == id);

            if (userToRemove != null)
            {
                _context.AppUsers.Remove(userToRemove);
                await _context.SaveChangesAsync();
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
