using MediatR;

namespace Application.Commands.Rooms_Commands
{
    public class DeleteImageFromRoomCommand : IRequest
    {
        public Guid RoomId { get; set; }
        public Guid ImageId { get; set; }
    }
}
