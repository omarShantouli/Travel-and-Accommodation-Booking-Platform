using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Payments
    {
        public Guid Id { get; set; }
        public Guid BookingId { get; set; }
        public string Method { get; set; }
        public string Status { get; set; }
        public double Amount { get; set; }
    }
}
