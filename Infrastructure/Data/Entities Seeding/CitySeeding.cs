using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Entities_Seeding
{
    public class CitySeeding
    {
        public static IEnumerable<City> SeedData()
        {
            return new List<City>
        {
            new City
            {
                Id = Guid.NewGuid(),
                Name = "New York",
                CountryName = "United States",
                PostOffice = "NYC",
                CountryCode = "US"
            },
            new City
            {
                Id = Guid.NewGuid(),
                Name = "London",
                CountryName = "United Kingdom",
                PostOffice = "LDN",
                CountryCode = "UK"
            },
            new City
            {
                Id = Guid.NewGuid(),
                Name = "Tokyo",
                CountryName = "Japan",
                PostOffice = "TKY",
                CountryCode = "JP"
            }
        };
        }
    }
}
