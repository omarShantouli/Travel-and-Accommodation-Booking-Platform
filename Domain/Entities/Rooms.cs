using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Rooms
    {
        public Guid Id { get; set; }
        public Guid HotelId { get; set; }
        public Guid RoomTypeId { get; set; }
        public int AdultsCapacity { get; set; }
        public int ChildrenCapacity { get; set; }
        public string View { get; set; }
        public float Rating { get; set; }
        public List<Bookings> Bookings { get; set; }
        public Hotels Hotel { get; set; }

    }
}
