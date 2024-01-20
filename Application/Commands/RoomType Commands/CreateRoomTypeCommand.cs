using Application.DTOs;
using MediatR;

namespace Application.Commands.RoomType_Commands
{
    public class CreateRoomTypeCommand : IRequest
    {
        public RoomTypeDto RoomType { get; set; }
    }
}
