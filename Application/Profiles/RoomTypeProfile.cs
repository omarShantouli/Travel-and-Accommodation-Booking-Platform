using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles
{
    public class RoomTypeProfile : Profile
    {
        public RoomTypeProfile()
        {
            CreateMap<RoomTypes, RoomTypeDto>();
            CreateMap<RoomTypeDto, RoomTypes>();
        }
    }
}
