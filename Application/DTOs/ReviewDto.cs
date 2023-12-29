using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ReviewDto
    {
        public Guid Id { get; set; }
        public Guid BookingId { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }
        public float Rating { get; set; }
    }
}
