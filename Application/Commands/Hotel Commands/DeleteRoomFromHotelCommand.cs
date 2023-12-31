using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class DeleteRoomFromHotelCommand : IRequest
    {
        public Guid HotelId { get; set; }
        public Guid RoomId { get; set; }
    }
}
