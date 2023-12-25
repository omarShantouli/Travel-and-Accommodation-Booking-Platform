using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Hotels
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public float Rating { get; set; }
        public string StreetAddress { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public int FloorsNumber { get; set; }
    }
}
