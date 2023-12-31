using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Hotel_Commands
{
    public class AddImageToHotelCommand : IRequest
    {
        public Guid HotelId { get; set; }
        public ImageDto Image { get; set; }
    }
}
