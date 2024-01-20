using Application.DTOs;
using MediatR;

namespace Application.Queries.RoomType_Queries
{
    public class GetAllRoomTypesQuery : IRequest<List<RoomTypeDto>>
    {
    }
}
