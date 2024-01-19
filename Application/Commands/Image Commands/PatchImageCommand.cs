using Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace Application.Commands.Image_Commands
{
    public class PatchImageCommand : IRequest
    {
        public Guid ImageId { get; set; }
        public JsonPatchDocument<ImageDto> patchDocument { get; set; }
    }
}
