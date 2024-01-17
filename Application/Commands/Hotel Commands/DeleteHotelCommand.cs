using MediatR;

namespace Application.Commands.Hotel_Commands
{
    public class DeleteHotelCommand : IRequest
    {
        public Guid HotelId { get; set; }
    }
}
