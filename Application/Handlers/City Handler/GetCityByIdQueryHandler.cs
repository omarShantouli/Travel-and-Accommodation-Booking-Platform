using Application.DTOs;
using Application.Queries.City_Queries;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers.City_Handler
{
    /// <summary>
    /// Handles the query to get a city by its ID.
    /// </summary>
    public class GetCityByIdQueryHandler : IRequestHandler<GetCityByIdQuery, CityDto>
    {
        private readonly IRepository<City> _cityRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCityByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetCityByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="cityRepository">The repository for city entities.</param>
        /// <param name="mapper">The AutoMapper for mapping entities to DTOs.</param>
        /// <param name="logger">The logger for capturing and logging information related to GetCityByIdQueryHandler.</param>
        public GetCityByIdQueryHandler(IRepository<City> cityRepository, IMapper mapper, ILogger<GetCityByIdQueryHandler> logger)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Handles the query to get a city by its ID.
        /// </summary>
        /// <param name="request">The query request containing the CityId.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The CityDto representing the city with the specified ID.</returns>
        public async Task<CityDto> Handle(GetCityByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Handling GetCityByIdQuery for CityId: {request.CityId}");

                var city = await _cityRepository.GetByIdAsync(request.CityId);

                if (city == null)
                {
                    _logger.LogWarning($"City with ID {request.CityId} not found.");
                    throw new EntityNotFoundException($"City with ID {request.CityId} not found.");
                }

                var cityDto = _mapper.Map<CityDto>(city);

                _logger.LogInformation($"GetCityByIdQuery handled successfully for CityId: {request.CityId}");

                return cityDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while handling GetCityByIdQuery for CityId: {request.CityId}");
                throw;
            }
        }
    }
}
