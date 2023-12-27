using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Entities_Seeding
{
    public class RoomsSeeding
    {
        public static IEnumerable<Rooms> SeedData()
        {
            return new List<Rooms>
        {
            new Rooms
            {
                Id = Guid.NewGuid(),
                HotelId = Guid.NewGuid(),
                RoomTypeId = Guid.NewGuid(),
                AdultsCapacity = 2,
                ChildrenCapacity = 1,
                View = "City View",
                Rating = 4.5f
            },
            new Rooms
            {
                Id = Guid.NewGuid(),
                HotelId = Guid.NewGuid(),
                RoomTypeId = Guid.NewGuid(),
                AdultsCapacity = 3,
                ChildrenCapacity = 2,
                View = "Ocean View",
                Rating = 4.2f
            },
            new Rooms
            {
                Id = Guid.NewGuid(),
                HotelId = Guid.NewGuid(),
                RoomTypeId = Guid.NewGuid(),
                AdultsCapacity = 4,
                ChildrenCapacity = 0,
                View = "Mountain View",
                Rating = 4.8f
            }
        };
        }
    }
}
