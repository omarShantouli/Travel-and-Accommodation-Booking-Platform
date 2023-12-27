using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Entities_Seeding
{
    public class HotelsSeeding
    {
        public static IEnumerable<Hotels> SeedData()
        {
            return new List<Hotels>
        {
            new Hotels
            {
                Id = Guid.NewGuid(),
                CityId = Guid.NewGuid(),
                OwnerId = Guid.NewGuid(),
                Name = "Luxury Inn",
                Rating = 4.5f,
                StreetAddress = "123 Main Street",
                Description = "A luxurious hotel with top-notch amenities.",
                Phone = "+1 123-456-7890",
                FloorsNumber = 10
            },
            new Hotels
            {
                Id = Guid.NewGuid(),
                CityId = Guid.NewGuid(),
                OwnerId = Guid.NewGuid(),
                Name = "Cozy Lodge",
                Rating = 3.8f,
                StreetAddress = "456 Oak Avenue",
                Description = "A cozy lodge nestled in the heart of nature.",
                Phone = "+44 20 1234 5678",
                FloorsNumber = 3
            },
            new Hotels
            {
                Id = Guid.NewGuid(),
                CityId = Guid.NewGuid(),
                OwnerId = Guid.NewGuid(),
                Name = "Sunset Resort",
                Rating = 4.2f,
                StreetAddress = "789 Beachfront Road",
                Description = "A resort with breathtaking sunset views over the ocean.",
                Phone = "+81 3-1234-5678",
                FloorsNumber = 5
            }
        };
        }
    }
}
