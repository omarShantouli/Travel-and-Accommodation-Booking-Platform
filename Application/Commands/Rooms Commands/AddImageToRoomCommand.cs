using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Rooms_Commands
{
    public class AddImageToRoomCommand : IRequest
    {
        public Guid RoomId { get; set; }
        public ImageDto Image { get; set; }
    }
}
