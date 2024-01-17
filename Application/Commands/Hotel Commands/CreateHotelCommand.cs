using Application.DTOs;
using MediatR;

namespace Application.Commands.Hotel_Commands
{
    public class CreateHotelCommand : IRequest
    {
        public HotelDto Hotel { get; set; }
    }
}
