using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Hotels
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
        public City City { get; set; }
        public Owner Owner { get; set; }
        public List<Rooms> Rooms { get; set; }
    }
}
