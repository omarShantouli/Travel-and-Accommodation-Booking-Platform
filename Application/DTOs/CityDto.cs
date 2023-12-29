using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CityDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CountryName { get; set; }
        public string PostOffice { get; set; }
        public string CountryCode { get; set; }
    }
}
