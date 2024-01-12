using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.City_Commands
{
    public class UpdateCityCommand : IRequest
    {
        public Guid CityId { get; set; }
        public CityDto UpdatedCity { get; set; }
    }
}
