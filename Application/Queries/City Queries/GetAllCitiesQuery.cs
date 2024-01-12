using Application.DTOs;
using MediatR;

namespace Application.Queries.City_Queries
{
    public class GetAllCitiesQuery : IRequest<List<CityDto>>
    {

    }
}
