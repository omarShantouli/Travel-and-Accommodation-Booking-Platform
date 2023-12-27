using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Entities_Seeding
{
    public class ReviewsSeeding
    {
        public static IEnumerable<Reviews> SeedData()
        {
            return new List<Reviews>
        {
            new Reviews
            {
                Id = Guid.NewGuid(),
                BookingId = Guid.NewGuid(),
                Comment = "Excellent service and comfortable stay!",
                ReviewDate = DateTime.Parse("2023-01-25"),
                Rating = 4.8f
            },
            new Reviews
            {
                Id = Guid.NewGuid(),
                BookingId = Guid.NewGuid(),
                Comment = "Friendly staff and great location.",
                ReviewDate = DateTime.Parse("2023-02-10"),
                Rating = 4.5f
            },
            new Reviews
            {
                Id = Guid.NewGuid(),
                BookingId = Guid.NewGuid(),
                Comment = "Clean rooms and beautiful views.",
                ReviewDate = DateTime.Parse("2023-03-18"),
                Rating = 4.2f
            }
        };
        }
    }
}
