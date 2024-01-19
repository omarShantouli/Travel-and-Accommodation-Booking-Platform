using Application.DTOs;
using MediatR;

namespace Application.Commands.Rooms_Commands
{
    public class CreateRoomCommand : IRequest
    {
        public RoomDto Room { get; set; }
    }
}
