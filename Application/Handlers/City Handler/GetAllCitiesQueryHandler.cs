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
    /// Handles the query to get all cities.
    /// </summary>
    public class GetAllCitiesQueryHandler : IRequestHandler<GetAllCitiesQuery, List<CityDto>>
    {
        private readonly IRepository<City> _cityRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllCitiesQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllCitiesQueryHandler"/> class.
        /// </summary>
        /// <param name="cityRepository">The repository for city entities.</param>
        /// <param name="mapper">The AutoMapper for mapping entities to DTOs.</param>
        /// <param name="logger">The logger for capturing and logging information related to GetAllCitiesQueryHandler.</param>
        public GetAllCitiesQueryHandler(IRepository<City> cityRepository, IMapper mapper, ILogger<GetAllCitiesQueryHandler> logger)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Handles the query to get all cities.
        /// </summary>
        /// <param name="request">The query request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of CityDto representing all cities.</returns>
        public async Task<List<CityDto>> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Handling GetAllCitiesQuery.");

                var cities = _cityRepository.GetAll();

                if (cities == null)
                {
                    _logger.LogWarning("No cities found.");
                    throw new EntityNotFoundException("There are no cities found.");
                }

                var citiesDto = _mapper.Map<List<CityDto>>(cities);

                _logger.LogInformation("GetAllCitiesQuery handled successfully.");

                return citiesDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while handling GetAllCitiesQuery.");
                throw;
            }
        }
    }
}
