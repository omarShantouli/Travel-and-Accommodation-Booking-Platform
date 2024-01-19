﻿using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Rooms_Queries
{
    public class GetAllRoomsQuery : IRequest<List<RoomDto>>
    {
    }
}
