using Application.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers
{
    public class AddHotelInCityCommandHandler : IRequestHandler<AddHotelInCityCommand>
    {
        private readonly IRepository<City> _cityRepository;
        private readonly IMapper _mapper;

        public AddHotelInCityCommandHandler(IRepository<City> cityRepository, IMapper mapper)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        public async Task Handle(AddHotelInCityCommand request, CancellationToken cancellationToken)
        {
            var city = _cityRepository.GetById(request.CityId);
            if (city == null)
            {
                throw new EntityNotFoundException($"City with ID {request.CityId} not found.");
            }

            var hotel = _mapper.Map<Hotels>(request.Hotel);

            city.Hotels.Add(hotel);
            await _cityRepository.SaveChangesAsync();
        }
    }
}
