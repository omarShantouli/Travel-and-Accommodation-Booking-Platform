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
                Id = 1,
                RoomId = 101,
                GuestId = 1001,
                CheckInDate = DateTime.Parse("2023-01-15"),
                CheckOutDate = DateTime.Parse("2023-01-20"),
                BookingDate = DateTime.Parse("2023-01-10")
            },
            new Bookings
            {
                Id = 2,
                RoomId = 102,
                GuestId = 1002,
                CheckInDate = DateTime.Parse("2023-02-05"),
                CheckOutDate = DateTime.Parse("2023-02-10"),
                BookingDate = DateTime.Parse("2023-01-28")
            },
            new Bookings
            {
                Id = 3,
                RoomId = 103,
                GuestId = 1003,
                CheckInDate = DateTime.Parse("2023-03-12"),
                CheckOutDate = DateTime.Parse("2023-03-18"),
                BookingDate = DateTime.Parse("2023-03-05")
            }
        };
        }
    }
}
