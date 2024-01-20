using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.RoomType_Queries
{
    public class GetRoomTypeByIdQuery : IRequest<RoomTypeDto>
    {
        public Guid RoomTypeId { get; set; }
    }
}
