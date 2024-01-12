using Application.Commands.City_Commands;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers.City_Handler
{
    /// <summary>
    /// Handles the command to apply partial updates to a city entity.
    /// </summary>
    public class PatchCityCommandHandler : IRequestHandler<PatchCityCommand>
    {
        private readonly IRepository<City> _cityRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PatchCityCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PatchCityCommandHandler"/> class.
        /// </summary>
        /// <param name="cityRepository">The repository for city entities.</param>
        /// <param name="mapper">The AutoMapper for mapping entities to DTOs.</param>
        /// <param name="logger">The logger for logging messages.</param>
        public PatchCityCommandHandler(IRepository<City> cityRepository, IMapper mapper, ILogger<PatchCityCommandHandler> logger)
        {
            _cityRepository = cityRepository ?? throw new ArgumentNullException(nameof(cityRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the command to apply partial updates to a city entity.
        /// </summary>
        /// <param name="request">The command request containing the CityId and patchDocument.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async Task Handle(PatchCityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Handling PatchCityCommand for CityId: {request.CityId}");

                var city = await _cityRepository.GetByIdAsync(request.CityId);

                if (city == null)
                {
                    _logger.LogWarning($"City with ID {request.CityId} not found.");
                    throw new EntityNotFoundException($"City with ID {request.CityId} not found.");
                }

                var cityDto = _mapper.Map<CityDto>(city);

                request.patchDocument.ApplyTo(cityDto);

                _mapper.Map(cityDto, city);

                await _cityRepository.SaveChangesAsync();

                _logger.LogInformation($"PatchCityCommand handled successfully for CityId: {request.CityId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while handling PatchCityCommand for CityId: {request.CityId}");
                throw;
            }
        }
    }
}
