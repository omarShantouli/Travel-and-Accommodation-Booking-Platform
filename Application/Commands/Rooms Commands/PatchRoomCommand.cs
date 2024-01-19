using Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Rooms_Commands
{
    public class PatchRoomCommand : IRequest
    {
        public Guid RoomId { get; set; }
        public JsonPatchDocument<RoomDto> patchDocument { get; set; }
    }
}
