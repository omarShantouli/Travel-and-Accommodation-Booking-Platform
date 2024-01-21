using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.RoomType_Queries
{
    public class GetRoomsOfRoomTypeQuery : IRequest<List<RoomDto>>
    {
        public Guid RoomTypeId { get; set; }
    }
}
