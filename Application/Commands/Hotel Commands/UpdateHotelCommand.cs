using Application.DTOs;
using MediatR;

namespace Application.Commands.Hotel_Commands
{
    public class UpdateHotelCommand : IRequest
    {
        public Guid HotelId { get; set; }
        public HotelDto UpdatedHotel { get; set; }
    }
}
