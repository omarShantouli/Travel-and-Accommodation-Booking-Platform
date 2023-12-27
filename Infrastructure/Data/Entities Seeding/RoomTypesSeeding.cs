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
                Id = Guid.NewGuid(),
                HotelId = Guid.NewGuid(),
                Type = RoomType.Single.ToString(),
                PricePerNight = 100.0f
            },
            new RoomTypes
            {
                Id = Guid.NewGuid(),
                HotelId = Guid.NewGuid(),
                Type = RoomType.Double.ToString(),
                PricePerNight = 150.0f
            },
            new RoomTypes
            {
                Id = Guid.NewGuid(),
                HotelId = Guid.NewGuid(),
                Type = RoomType.Suite.ToString(),
                PricePerNight = 200.0f
            }
        };
        }
    }
}
