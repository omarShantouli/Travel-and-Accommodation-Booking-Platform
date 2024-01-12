using Application.Commands.City_Commands;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using static Domain.Interfaces.IRepository;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.City_Handler
{
    /// <summary>
    /// Handles the command to update a city entity.
    /// </summary>
    public class UpdateCityCommandHandler : IRequestHandler<UpdateCityCommand>
    {
        private readonly IRepository<City> _cityRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCityCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateCityCommandHandler"/> class.
        /// </summary>
        /// <param name="cityRepository">The repository for city entities.</param>
        /// <param name="mapper">The AutoMapper for mapping entities to DTOs.</param>
        /// <param name="logger">The logger for logging messages.</param>
        public UpdateCityCommandHandler(IRepository<City> cityRepository, IMapper mapper, ILogger<UpdateCityCommandHandler> logger)
        {
            _cityRepository = cityRepository ?? throw new ArgumentNullException(nameof(cityRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the command to update a city entity.
        /// </summary>
        /// <param name="request">The command request containing the CityId and UpdatedCity.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async Task Handle(UpdateCityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Handling UpdateCityCommand for CityId: {request.CityId}");

                var existingCity = await _cityRepository.GetByIdAsync(request.CityId);

                if (existingCity == null)
                {
                    _logger.LogWarning($"City with ID {request.CityId} not found.");
                    throw new EntityNotFoundException($"City with ID {request.CityId} not found.");
                }

                _mapper.Map(request.UpdatedCity, existingCity);

                await _cityRepository.SaveChangesAsync();

                _logger.LogInformation($"UpdateCityCommand handled successfully for CityId: {request.CityId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while handling UpdateCityCommand for CityId: {request.CityId}");
                throw;
            }
        }
    }
}
