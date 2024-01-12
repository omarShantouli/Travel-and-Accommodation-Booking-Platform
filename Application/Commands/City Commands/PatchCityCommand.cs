using Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace Application.Commands.City_Commands
{
    public class PatchCityCommand : IRequest
    {
        public Guid CityId { get; set; }
        public JsonPatchDocument<CityDto> patchDocument { get; set; }
    }
}
