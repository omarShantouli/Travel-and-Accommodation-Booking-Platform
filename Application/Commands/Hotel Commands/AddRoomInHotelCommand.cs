using Application.DTOs;
using MediatR;

namespace Application.Commands
{
    public class AddRoomInHotelCommand : IRequest
    {
        public Guid HotelId { get; set; }
        public RoomDto Room { get; set; }
    }
}
