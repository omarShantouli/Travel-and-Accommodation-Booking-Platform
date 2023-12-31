using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles
{
    public class RoomsProfile : Profile
    {
        public RoomsProfile()
        {
            CreateMap<RoomDto, Rooms>();
            CreateMap<Rooms, RoomDto>();
        }
    }
}
