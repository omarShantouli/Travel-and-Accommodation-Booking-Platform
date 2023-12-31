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
                Id = new Guid("98c2c9fe-1a1c-4eaa-a7f5-b9d19b246c27"),
                CityId = new Guid("f9e85d04-548c-4f98-afe9-2a8831c62a90"),
                OwnerId = new Guid("a1d1aa11-12e7-4e0f-8425-67c1c1e62c2d"),
                Name = "Luxury Inn",
                Rating = 4.5f,
                StreetAddress = "123 Main Street",
                Description = "A luxurious hotel with top-notch amenities.",
                Phone = "1234567890",
                FloorsNumber = 10
            },
            new Hotels
            {
                Id = new Guid("bfa4173d-7893-48b9-a497-5f4c7fb2492b"),
                CityId = new Guid("8d2aeb7a-7c67-4911-aa2c-d6a3b4dc7e9e"),
                OwnerId = new Guid("a1d1aa11-12e7-4e0f-8425-67c1c1e62c2d"),
                Name = "Cozy Lodge",
                Rating = 3.8f,
                StreetAddress = "456 Oak Avenue",
                Description = "A cozy lodge nestled in the heart of nature.",
                Phone = "2012345678",
                FloorsNumber = 3
            },
            new Hotels
            {
                Id = new Guid("9461e08b-92d3-45da-b6b3-efc0cfcc4a3a"),
                CityId = new Guid("3c7e66f5-46a9-4b8d-8e90-85b5a9e2c2fd"),
                OwnerId = new Guid("a1d1aa11-12e7-4e0f-8425-67c1c1e62c2d"),
                Name = "Sunset Resort",
                Rating = 4.2f,
                StreetAddress = "789 Beachfront Road",
                Description = "A resort with breathtaking sunset views over the ocean.",
                Phone = "312345678",
                FloorsNumber = 5
            }
        };
        }
    }
}
