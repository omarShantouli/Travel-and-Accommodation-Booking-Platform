using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RoomTypes
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string Type { get; set; }
        public float PricePerNight { get; set; }
    }
}
