using MediatR;

namespace Application.Commands.RoomType_Commands
{
    public class DeleteRoomTypeCommand : IRequest
    {
        public Guid RoomTypeId { get; set; }
    }
}
