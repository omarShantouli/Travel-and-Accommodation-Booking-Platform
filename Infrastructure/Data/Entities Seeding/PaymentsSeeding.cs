using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Entities_Seeding
{
    public class PaymentsSeeding
    {
        public static IEnumerable<Payments> SeedData()
        {
            return new List<Payments>
        {
            new Payments
            {
                Id = Guid.NewGuid(),
                BookingId = Guid.NewGuid(),
                Method = "Credit Card",
                Status = PaymentStatus.Completed.ToString(),
                Amount = 1500.0
            },
            new Payments
            {
                Id = Guid.NewGuid(),
                BookingId = Guid.NewGuid(),
                Method = "PayPal",
                Status = PaymentStatus.Pending.ToString(),
                Amount = 1200.0
            },
            new Payments
            {
                Id = Guid.NewGuid(),
                BookingId = Guid.NewGuid(),
                Method = "Bank Transfer",
                Status = PaymentStatus.Completed.ToString(),
                Amount = 2000.0
            }
        };
        }
    }
}
