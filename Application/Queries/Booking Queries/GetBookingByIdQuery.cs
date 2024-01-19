using Application.DTOs;
using MediatR;

namespace Application.Queries.Booking_Queries
{
    public class GetBookingByIdQuery : IRequest<BookingDto>
    {
        public Guid BookingId { get; set; }
    }
}
