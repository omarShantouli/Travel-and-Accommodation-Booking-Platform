using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class HotelDto
    {
        public Guid Id { get; set; }
        public Guid CityId { get; set; }
        public Guid OwnerId { get; set; }
        public string Name { get; set; }
        public float Rating { get; set; }
        public string StreetAddress { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public int FloorsNumber { get; set; }
    }
}
