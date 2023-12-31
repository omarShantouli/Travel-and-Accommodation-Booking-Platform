using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Entities_Seeding
{
    public class RoomTypesSeeding
    {
        public static IEnumerable<RoomTypes> SeedData()
        {
            return new List<RoomTypes>
        {
            new RoomTypes
            {
                Id = new Guid("5a5de3b8-3ed8-4f0a-bda9-cf73225a64a1"),
                HotelId = new Guid("98c2c9fe-1a1c-4eaa-a7f5-b9d19b246c27"),
                Type = RoomType.Single.ToString(),
                PricePerNight = 100.0f
            },
            new RoomTypes
            {
                Id = new Guid("d67ddbe4-1f1a-4d85-bcc1-ec3a475ecb68"),
                HotelId = new Guid("bfa4173d-7893-48b9-a497-5f4c7fb2492b"),
                Type = RoomType.Double.ToString(),
                PricePerNight = 150.0f
            },
            new RoomTypes
            {
                Id = new Guid("4b4c0ea5-0b9a-4a20-8ad9-77441fb912d2"),
                HotelId = new Guid("9461e08b-92d3-45da-b6b3-efc0cfcc4a3a"),
                Type = RoomType.Suite.ToString(),
                PricePerNight = 200.0f
            }
        };
        }
    }
}
