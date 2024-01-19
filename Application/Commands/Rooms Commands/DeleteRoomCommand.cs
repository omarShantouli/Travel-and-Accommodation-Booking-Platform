using MediatR;

namespace Application.Commands.Rooms_Commands
{
    public class DeleteRoomCommand : IRequest
    {
        public Guid RoomId { get; set; }
    }
}
