using Domain.Entities;
using Domain.Exceptions;
using static Domain.Interfaces.IRepository;

namespace Infrastructure.Data.Repositories
{
    public class RoomTypesRepository : IRepository<RoomTypes>, IDisposable
    {
        private readonly ApplicationDbContext _context;

        public RoomTypesRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public RoomTypes GetById(Guid id)
        {
            var roomType = _context.RoomTypes.FirstOrDefault(rt => rt.Id == id);

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

        public void Create(RoomTypes roomType)
        {
            if (roomType == null)
            {
                throw new ArgumentNullException(nameof(roomType));
            }

            _context.RoomTypes.Add(roomType);
            _context.SaveChanges();
        }

        public void Update(RoomTypes roomType)
        {
            if (roomType == null)
            {
                throw new ArgumentNullException(nameof(roomType));
            }

            var existingRoomType = _context.RoomTypes.FirstOrDefault(rt => rt.Id == roomType.Id);

            if (existingRoomType == null)
            {
                throw new EntityNotFoundException($"RoomType with ID {roomType.Id} not found.");
            }

            existingRoomType.HotelId = roomType.HotelId;
            existingRoomType.Type = roomType.Type;
            existingRoomType.PricePerNight = roomType.PricePerNight;

            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var roomTypeToRemove = _context.RoomTypes.FirstOrDefault(rt => rt.Id == id);

            if (roomTypeToRemove == null)
            {
                throw new EntityNotFoundException($"RoomType with ID {id} not found.");
            }

            _context.RoomTypes.Remove(roomTypeToRemove);
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
