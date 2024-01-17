using Application.DTOs;
using MediatR;

namespace Application.Queries.Hotels_Queries
{
    public class GetHotelByIdQuery : IRequest<HotelDto>
    {
        public Guid HotelId { get; set; }
    }
}
