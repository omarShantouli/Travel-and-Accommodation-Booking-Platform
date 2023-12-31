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
                Id = new Guid("a98b8a9d-4c5a-4a90-a2d2-5f1441b93db6"),
                HotelId = new Guid("98c2c9fe-1a1c-4eaa-a7f5-b9d19b246c27"),
                RoomTypeId = new Guid("5a5de3b8-3ed8-4f0a-bda9-cf73225a64a1"),
                AdultsCapacity = 2,
                ChildrenCapacity = 1,
                View = "City View",
                Rating = 4.5f
            },
            new Rooms
            {
                Id = new Guid("4e1cb3d9-bc3b-4997-a3d5-0c56cf17fe7a"),
                HotelId = new Guid("bfa4173d-7893-48b9-a497-5f4c7fb2492b"),
                RoomTypeId = new Guid("d67ddbe4-1f1a-4d85-bcc1-ec3a475ecb68"),
                AdultsCapacity = 3,
                ChildrenCapacity = 2,
                View = "Ocean View",
                Rating = 4.2f
            },
            new Rooms
            {
                Id = new Guid("c6898b7e-ee09-4b36-8b20-22e8c2a63e29"),
                HotelId = new Guid("9461e08b-92d3-45da-b6b3-efc0cfcc4a3a"),
                RoomTypeId = new Guid("4b4c0ea5-0b9a-4a20-8ad9-77441fb912d2"),
                AdultsCapacity = 4,
                ChildrenCapacity = 0,
                View = "Mountain View",
                Rating = 4.8f
            }
        };
        }
    }
}
