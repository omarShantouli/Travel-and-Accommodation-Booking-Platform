using Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.RoomType_Commands
{
    public class PatchRoomTypeCommand : IRequest
    {
        public Guid RoomTypeId { get; set; }
        public JsonPatchDocument<RoomTypeDto> patchDocument { get; set; }
    }
}
