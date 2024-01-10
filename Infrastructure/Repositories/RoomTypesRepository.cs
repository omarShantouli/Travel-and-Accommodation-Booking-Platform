using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using static Domain.Interfaces.IRepository;

namespace Infrastructure.Repositories
{
    public class RoomTypesRepository : IRepository<RoomTypes>, IDisposable
    {
        private readonly ApplicationDbContext _context;

        public RoomTypesRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<RoomTypes> GetByIdAsync(Guid id)
        {
            var roomType = await _context.RoomTypes.FirstOrDefaultAsync(rt => rt.Id == id);

            if (roomType == null)
            {
                throw new EntityNotFoundException($"RoomType with ID {id} not found.");
            }

            return roomType;
        }

        public IEnumerable<RoomTypes> GetAll()
        {
            return _context.RoomTypes;
        }

        public async Task<RoomTypes> CreateAsync(RoomTypes roomType)
        {
            if (roomType == null)
            {
                throw new ArgumentNullException(nameof(roomType));
            }

            _context.RoomTypes.Add(roomType);
            await _context.SaveChangesAsync();
            return roomType;
        }

        public async Task UpdateAsync(RoomTypes roomType)
        {
            if (roomType == null)
            {
                throw new ArgumentNullException(nameof(roomType));
            }

            var existingRoomType = await _context.RoomTypes.FirstOrDefaultAsync(rt => rt.Id == roomType.Id);

            if (existingRoomType == null)
            {
                throw new EntityNotFoundException($"RoomType with ID {roomType.Id} not found.");
            }

            existingRoomType.HotelId = roomType.HotelId;
            existingRoomType.Type = roomType.Type;
            existingRoomType.PricePerNight = roomType.PricePerNight;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var roomTypeToRemove = _context.RoomTypes.FirstOrDefault(rt => rt.Id == id);

            if (roomTypeToRemove == null)
            {
                throw new EntityNotFoundException($"RoomType with ID {id} not found.");
            }

            _context.RoomTypes.Remove(roomTypeToRemove);
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
