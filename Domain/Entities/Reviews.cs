using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Reviews
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }
        public float Rating { get; set; }

    }
}
