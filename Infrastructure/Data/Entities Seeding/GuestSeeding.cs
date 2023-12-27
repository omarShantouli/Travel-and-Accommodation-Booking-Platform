using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Entities_Seeding
{
    public  class GuestSeeding
    {
        public static IEnumerable<Guest> SeedData()
        {
            return new List<Guest>
        {
            new Guest
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Phone = "+1 123-456-7890"
            },
            new Guest
            {
                Id = Guid.NewGuid(),
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@example.com",
                Phone = "+44 20 1234 5678"
            },
            new Guest
            {
                Id = Guid.NewGuid(),
                FirstName = "Hiroshi",
                LastName = "Tanaka",
                Email = "hiroshi.tanaka@example.co.jp",
                Phone = "+81 3-1234-5678"
            }
        };
        }
    }
}
