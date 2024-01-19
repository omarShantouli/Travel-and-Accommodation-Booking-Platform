using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Rooms_Commands
{
    public class UpdateRoomCommand : IRequest
    {
        public Guid RoomId { get; set; }
        public RoomDto UpdatedRoom { get; set; }
    }
}
