using Application.DTOs;
using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetHotelsInCityQuery : IRequest<List<HotelDto>>
    {
        public Guid CityId { get; set; }
    }
}
