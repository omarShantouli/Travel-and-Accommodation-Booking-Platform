using Application.Commands.City_Commands;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers.City_Handler
{
    /// <summary>
    /// Handles the command to delete a city.
    /// </summary>
    public class DeleteCityCommandHandler : IRequestHandler<DeleteCityCommand>
    {
        private readonly IRepository<City> _cityRepository;
        private readonly ILogger<DeleteCityCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCityCommandHandler"/> class.
        /// </summary>
        /// <param name="cityRepository">The repository for city entities.</param>
        /// <param name="logger">The logger for capturing and logging information related to DeleteCityCommandHandler.</param>
        public DeleteCityCommandHandler(IRepository<City> cityRepository, ILogger<DeleteCityCommandHandler> logger)
        {
            _cityRepository = cityRepository;
            _logger = logger;
        }

        /// <summary>
        /// Handles the command to delete a city.
        /// </summary>
        /// <param name="request">The command request containing city ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public async Task Handle(DeleteCityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Handling DeleteCityCommand for CityId: {request.CityId}");

                var existingCity = await _cityRepository.GetByIdAsync(request.CityId);

                if (existingCity == null)
                {
                    _logger.LogWarning($"City with ID {request.CityId} not found.");
                    throw new EntityNotFoundException($"City with ID {request.CityId} not found.");
                }

                await _cityRepository.DeleteAsync(existingCity.Id);

                await _cityRepository.SaveChangesAsync();

                _logger.LogInformation($"DeleteCityCommand handled successfully for CityId: {request.CityId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while handling DeleteCityCommand for CityId: {request.CityId}");
                throw;
            }
        }
    }
}
