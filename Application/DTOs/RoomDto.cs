using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class RoomDto
    {
        public Guid Id { get; set; }
        public Guid HotelId { get; set; }
        public Guid RoomTypeId { get; set; }
        public int AdultsCapacity { get; set; }
        public int ChildrenCapacity { get; set; }
        public string View { get; set; }
        public float Rating { get; set; }
    }
}
