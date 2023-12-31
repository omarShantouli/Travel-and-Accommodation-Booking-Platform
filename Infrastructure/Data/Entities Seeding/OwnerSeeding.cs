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
                Id = new Guid("a1d1aa11-12e7-4e0f-8425-67c1c1e62c2d"),
                FirstName = "Omar",
                LastName = "Hantouli",
                Email = "hantoli797@gmail.com",
                Phone = "0598191973",
            };
        }
    }
}
