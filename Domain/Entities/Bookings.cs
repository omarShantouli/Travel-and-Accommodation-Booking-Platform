using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Bookings
    {
        public Guid Id { get; set; }
        public Guid RoomId { get; set; }
        public Guid GuestId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime BookingDate { get; set; }
        public Guest Guest { get; set; }
        public Rooms Room { get; set; }
        public Reviews? Review { get; set; }
    }
}
