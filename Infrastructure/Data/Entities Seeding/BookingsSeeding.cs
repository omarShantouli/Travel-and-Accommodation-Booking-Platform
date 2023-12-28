using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Entities_Seeding
{
    public class BookingsSeeding
    {
        public static IEnumerable<Bookings> SeedData()
        {
            return new List<Bookings>
        {
            new Bookings
            {
                Id = Guid.NewGuid(),
                RoomId = Guid.NewGuid(),
                GuestId = Guid.NewGuid(),
                CheckInDate = DateTime.Parse("2023-01-15"),
                CheckOutDate = DateTime.Parse("2023-01-20"),
                BookingDate = DateTime.Parse("2023-01-10")
            },
            new Bookings
            {
                Id = Guid.NewGuid(),
                RoomId = Guid.NewGuid(),
                GuestId = Guid.NewGuid(),
                CheckInDate = DateTime.Parse("2023-02-05"),
                CheckOutDate = DateTime.Parse("2023-02-10"),
                BookingDate = DateTime.Parse("2023-01-28")
            },
            new Bookings
            {
                Id = Guid.NewGuid(),
                RoomId = Guid.NewGuid(),
                GuestId = Guid.NewGuid(),
                CheckInDate = DateTime.Parse("2023-03-12"),
                CheckOutDate = DateTime.Parse("2023-03-18"),
                BookingDate = DateTime.Parse("2023-03-05")
            }
        };
        }
    }
}
