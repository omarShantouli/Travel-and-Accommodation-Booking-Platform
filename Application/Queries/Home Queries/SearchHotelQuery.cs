using Application.DTOs;
using MediatR;

namespace Application.Queries.Home
{
    public class SearchHotelQuery : IRequest<List<HotelDto>>
    {
        public string City { get; set; }
        public float StarRate { get; set; }
        public int ChildrenCapacity { get; set; }
        public int AdultsCapacity { get; set; }
        public string CheckOutDate { get; set; }
        public string CheckInDate { get; set; }
        public int NumberOfRooms { get; set; }

    }
}
