using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class RoomTypeDto
    {
        public Guid Id { get; set; }
        public Guid HotelId { get; set; }
        public string Type { get; set; }
        public float PricePerNight { get; set; }
    }
}
