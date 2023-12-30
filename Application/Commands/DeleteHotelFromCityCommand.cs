using MediatR;

namespace Application.Commands
{
    public class DeleteHotelFromCityCommand : IRequest
    {
        public Guid CityId { get; set; }
        public Guid HotelId { get; set; }
    }
}
