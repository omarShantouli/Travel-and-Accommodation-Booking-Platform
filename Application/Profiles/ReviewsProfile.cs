using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles
{
    public class ReviewsProfile : Profile
    {
        public ReviewsProfile()
        {
            CreateMap<ReviewDto, Reviews>();
            CreateMap<Reviews, ReviewDto>();
        }
    }
}
