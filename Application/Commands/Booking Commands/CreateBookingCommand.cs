using Application.DTOs;
using MediatR;

namespace Application.Commands.Booking_Commands
{
    public class CreateBookingCommand : IRequest
    {
        public BookingDto Booking { get; set; }
    }
}
