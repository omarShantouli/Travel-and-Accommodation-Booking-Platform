using Domain.Entities;
using Domain.Exceptions;
using static Domain.Interfaces.IRepository;

namespace Infrastructure.Data.Repositories
{
    public class RoomsRepository : IRepository<Rooms>, IDisposable
    {
        private readonly ApplicationDbContext _context;

        public RoomsRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Rooms GetById(Guid id)
        {
            var room = _context.Rooms.FirstOrDefault(r => r.Id == id);

            if (room == null)
            {
                throw new EntityNotFoundException($"Room with ID {id} not found.");
            }

            return room;
        }

        public IEnumerable<Rooms> GetAll()
        {
            return _context.Rooms;
        }

        public void Create(Rooms room)
        {
            if (room == null)
            {
                throw new ArgumentNullException(nameof(room));
            }

            _context.Rooms.Add(room);
            _context.SaveChanges();
        }

        public void Update(Rooms room)
        {
            if (room == null)
            {
                throw new ArgumentNullException(nameof(room));
            }

            var existingRoom = _context.Rooms.FirstOrDefault(r => r.Id == room.Id);

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

            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var roomToRemove = _context.Rooms.FirstOrDefault(r => r.Id == id);

            if (roomToRemove == null)
            {
                throw new EntityNotFoundException($"Room with ID {id} not found.");
            }

            _context.Rooms.Remove(roomToRemove);
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
