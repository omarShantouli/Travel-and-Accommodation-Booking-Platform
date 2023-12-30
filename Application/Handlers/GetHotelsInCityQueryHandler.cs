using Application.DTOs;
using Application.Queries;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers
{
    public class GetHotelsInCityQueryHandler : IRequestHandler<GetHotelsInCityQuery, List<HotelDto>>
    {
        private readonly IRepository<City> _cityRepository;
        private readonly IMapper _mapper;

        public GetHotelsInCityQueryHandler(IRepository<City> cityRepository, IMapper mapper)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        public async Task<List<HotelDto>> Handle(GetHotelsInCityQuery request, CancellationToken cancellationToken)
        {
            var city = _cityRepository.GetById(request.CityId);
            if (city == null) 
            { 
                throw new EntityNotFoundException($"City with ID {request.CityId} not found.");
            }

            var hotelsInCity = city.Hotels;

            var hotelDtos = _mapper.Map<List<HotelDto>>(hotelsInCity);

            return hotelDtos;
        }
    }
}
