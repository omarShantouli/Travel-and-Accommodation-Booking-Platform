using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class AddImageToCityCommand : IRequest
    {
        public Guid CityId { get; set; }
        public ImageDto Image { get; set; }
    }
}
