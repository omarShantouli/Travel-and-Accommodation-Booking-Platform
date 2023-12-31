using Application.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers
{
    public class DeleteHotelFromCityCommandHandler : IRequestHandler<DeleteHotelFromCityCommand>
    {
        private readonly IRepository<City> _cityRepository;
        private readonly IMapper _mapper;

        public DeleteHotelFromCityCommandHandler(IRepository<City> cityRepository, IMapper mapper)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        public async Task Handle(DeleteHotelFromCityCommand request, CancellationToken cancellationToken)
        {
            var city = _cityRepository.GetById(request.CityId);
            if (city == null)
            {
                throw new EntityNotFoundException($"City with ID {request.CityId} not found.");
            }

            var hotelToRemove = city.Hotels.FirstOrDefault(h => h.Id == request.HotelId);
            if (hotelToRemove == null)
            {
                throw new EntityNotFoundException($"Hotel with ID {request.HotelId} not found in City with ID {request.CityId}.");

            }

            city.Hotels.Remove(hotelToRemove);

            await _cityRepository.SaveChangesAsync();
        }
    }
}
