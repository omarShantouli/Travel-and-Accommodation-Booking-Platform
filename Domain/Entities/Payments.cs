using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Payments
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public string Method { get; set; }
        public string Status { get; set; }
        public Type Amount { get; set; }
    }
}
