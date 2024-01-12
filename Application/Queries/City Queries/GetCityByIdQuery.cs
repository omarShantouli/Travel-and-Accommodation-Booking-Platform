using Application.DTOs;
using MediatR;

namespace Application.Queries.City_Queries
{
    public class GetCityByIdQuery : IRequest<CityDto>
    {
        public Guid CityId { get; set; }
    }
}
