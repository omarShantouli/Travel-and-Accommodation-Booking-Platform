using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles
{
    public class HotelsProfile : Profile
    {
        public HotelsProfile()
        {
            CreateMap<Hotels, HotelDto>();
            CreateMap<HotelDto, Hotels>();
        }
    }
}
