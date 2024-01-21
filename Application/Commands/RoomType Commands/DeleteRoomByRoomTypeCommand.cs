using MediatR;

namespace Application.Commands.RoomType_Commands
{
    public class DeleteRoomByRoomTypeCommand : IRequest
    {
        public Guid RoomId { get; set; }
        public Guid RoomTypeId { get; set; }
    }
}
