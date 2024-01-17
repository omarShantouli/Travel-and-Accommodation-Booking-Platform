using Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace Application.Commands.Hotel_Commands
{
    public class PatchHotelCommand : IRequest
    {
        public Guid HotelId { get; set; }
        public JsonPatchDocument<HotelDto> patchDocument { get; set; }
    }
}
