using Application.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers
{
    public class AddHotelInCityCommandHandler : IRequestHandler<AddHotelInCityCommand>
    {
        private readonly IRepository<City> _cityRepository;
        private readonly IRepository<Hotels> _hotelRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AddHotelInCityCommandHandler> _logger;

        public AddHotelInCityCommandHandler(IRepository<City> cityRepository,
                                            IRepository<Hotels> hotelRepository, IMapper mapper,
                                            ILogger<AddHotelInCityCommandHandler> logger)
        {
            _cityRepository = cityRepository;
            _hotelRepository = hotelRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Handles the command to add a hotel in a city.
        /// </summary>
        /// <param name="request">The command request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public async Task Handle(AddHotelInCityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Handling AddHotelInCityCommand for CityId: {request.CityId}");

                var city = await _cityRepository.GetByIdAsync(request.CityId);
                if (city == null)
                {
                    _logger.LogWarning($"City with ID {request.CityId} not found.");
                    throw new EntityNotFoundException($"City with ID {request.CityId} not found.");
                }

                var hotel = _mapper.Map<Hotels>(request.Hotel);

                await _hotelRepository.CreateAsync(hotel);
                await _hotelRepository.SaveChangesAsync();

                city.Hotels.Add(hotel);
                await _cityRepository.SaveChangesAsync();

                _logger.LogInformation($"AddHotelInCityCommand handled successfully for CityId: {request.CityId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while handling AddHotelInCityCommand for CityId: {request.CityId}");
                throw;
            }
        }
    }
}
