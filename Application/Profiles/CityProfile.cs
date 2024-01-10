using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<City, CityDto>();
            CreateMap<CityDto, City>();
        }
    }
}
