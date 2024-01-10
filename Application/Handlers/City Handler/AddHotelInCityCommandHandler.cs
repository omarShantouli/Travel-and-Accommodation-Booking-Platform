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
        private readonly IRepository<Hotels> _hotelRepository;
        private readonly IMapper _mapper;

        public AddHotelInCityCommandHandler(IRepository<City> cityRepository,
                                            IRepository<Hotels> hotelRepository, IMapper mapper)
        {
            _cityRepository = cityRepository;
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task Handle(AddHotelInCityCommand request, CancellationToken cancellationToken)
        {
            var city = await _cityRepository.GetByIdAsync(request.CityId);
            if (city == null)
            {
                throw new EntityNotFoundException($"City with ID {request.CityId} not found.");
            }

            var hotel = _mapper.Map<Hotels>(request.Hotel);

            await _hotelRepository.CreateAsync(hotel);
            await _hotelRepository.SaveChangesAsync();

            city.Hotels.Add(hotel);
            await _cityRepository.SaveChangesAsync();
        }
    }
}
