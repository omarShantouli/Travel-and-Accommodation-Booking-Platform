using MediatR;

namespace Application.Commands.City_Commands
{
    public class DeleteCityCommand : IRequest
    {
        public Guid CityId { get; set; }
    }
}
