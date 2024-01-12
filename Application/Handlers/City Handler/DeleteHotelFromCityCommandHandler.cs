using Application.Commands;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers
{
    /// <summary>
    /// Handles the command to delete a hotel from a city.
    /// </summary>
    public class DeleteHotelFromCityCommandHandler : IRequestHandler<DeleteHotelFromCityCommand>
    {
        private readonly IRepository<City> _cityRepository;
        private readonly ILogger<DeleteHotelFromCityCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteHotelFromCityCommandHandler"/> class.
        /// </summary>
        /// <param name="cityRepository">The repository for city entities.</param>
        /// <param name="logger">The logger for capturing and logging information related to DeleteHotelFromCityCommandHandler.</param>
        public DeleteHotelFromCityCommandHandler(IRepository<City> cityRepository, ILogger<DeleteHotelFromCityCommandHandler> logger)
        {
            _cityRepository = cityRepository;
            _logger = logger;
        }

        /// <summary>
        /// Handles the command to delete a hotel from a city.
        /// </summary>
        /// <param name="request">The command request containing city and hotel IDs.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public async Task Handle(DeleteHotelFromCityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Handling DeleteHotelFromCityCommand for CityId: {request.CityId}, HotelId: {request.HotelId}");

                var city = await _cityRepository.GetByIdAsync(request.CityId);
                if (city == null)
                {
                    _logger.LogWarning($"City with ID {request.CityId} not found.");
                    throw new EntityNotFoundException($"City with ID {request.CityId} not found.");
                }

                var hotelToRemove = city.Hotels.FirstOrDefault(h => h.Id == request.HotelId);
                if (hotelToRemove == null)
                {
                    _logger.LogWarning($"Hotel with ID {request.HotelId} not found in City with ID {request.CityId}.");
                    throw new EntityNotFoundException($"Hotel with ID {request.HotelId} not found in City with ID {request.CityId}.");
                }

                city.Hotels.Remove(hotelToRemove);

                await _cityRepository.SaveChangesAsync();

                _logger.LogInformation($"DeleteHotelFromCityCommand handled successfully for CityId: {request.CityId}, HotelId: {request.HotelId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while handling DeleteHotelFromCityCommand for CityId: {request.CityId}, HotelId: {request.HotelId}");
                throw;
            }
        }
    }
}
