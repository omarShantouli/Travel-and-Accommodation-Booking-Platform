using Application.DTOs;
using MediatR;

namespace Application.Commands.RoomType_Commands
{
    public class UpdateRoomTypeCommand : IRequest
    {
        public Guid RoomTypeId { get; set; }
        public RoomTypeDto UpdatedRoomType { get; set; }
    }
}
