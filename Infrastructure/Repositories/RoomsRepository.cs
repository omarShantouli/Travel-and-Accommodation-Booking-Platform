using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using static Domain.Interfaces.IRepository;

namespace Infrastructure.Repositories
{
    public class RoomsRepository : IRepository<Rooms>, IDisposable
    {
        private readonly ApplicationDbContext _context;

        public RoomsRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Rooms> GetByIdAsync(Guid id)
        {
            var room = await _context.Rooms.Include(r => r.Hotel).Include(r => r.Bookings).FirstOrDefaultAsync(r => r.Id == id);

            if (room == null)
            {
                throw new EntityNotFoundException($"Room with ID {id} not found.");
            }

            return room;
        }

        public IEnumerable<Rooms> GetAll()
        {
            return _context.Rooms.Include(r => r.Hotel).Include(r => r.Bookings);
        }


        public async Task<Rooms> CreateAsync(Rooms room)
        {
            if (room == null)
            {
                throw new ArgumentNullException(nameof(room));
            }

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task UpdateAsync(Rooms room)
        {
            if (room == null)
            {
                throw new ArgumentNullException(nameof(room));
            }

            var existingRoom = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == room.Id);

            if (existingRoom == null)
            {
                throw new EntityNotFoundException($"Room with ID {room.Id} not found.");
            }

            existingRoom.HotelId = room.HotelId;
            existingRoom.RoomTypeId = room.RoomTypeId;
            existingRoom.AdultsCapacity = room.AdultsCapacity;
            existingRoom.ChildrenCapacity = room.ChildrenCapacity;
            existingRoom.View = room.View;
            existingRoom.Rating = room.Rating;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var roomToRemove = _context.Rooms.FirstOrDefault(r => r.Id == id);

            if (roomToRemove == null)
            {
                throw new EntityNotFoundException($"Room with ID {id} not found.");
            }

            _context.Rooms.Remove(roomToRemove);
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
