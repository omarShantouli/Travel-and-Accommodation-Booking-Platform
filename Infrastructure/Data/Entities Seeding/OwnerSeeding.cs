using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Entities_Seeding
{
    public class OwnerSeeding
    {
        public static Owner SeedData()
        {
            return new Owner
            {
                Id = Guid.NewGuid(),
                FirstName = "Omar",
                LastName = "Hantouli",
                Email = "hantoli797@gmail.com",
                Phone = "+972598191973",
            };
        }
    }
}
