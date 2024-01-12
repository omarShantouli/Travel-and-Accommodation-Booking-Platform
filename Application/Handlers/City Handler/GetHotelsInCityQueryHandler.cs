using Application.DTOs;
using Application.Queries;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers
{
    /// <summary>
    /// Handles the query to retrieve a list of hotels in a city.
    /// </summary>
    public class GetHotelsInCityQueryHandler : IRequestHandler<GetHotelsInCityQuery, List<HotelDto>>
    {
        private readonly IRepository<City> _cityRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetHotelsInCityQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetHotelsInCityQueryHandler"/> class.
        /// </summary>
        /// <param name="cityRepository">The repository for city entities.</param>
        /// <param name="mapper">The AutoMapper for mapping entities to DTOs.</param>
        /// <param name="logger">The logger for capturing and logging information related to GetHotelsInCityQueryHandler.</param>
        public GetHotelsInCityQueryHandler(IRepository<City> cityRepository, IMapper mapper, ILogger<GetHotelsInCityQueryHandler> logger)
        {
            _cityRepository = cityRepository ?? throw new ArgumentNullException(nameof(cityRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger;
        }

        /// <summary>
        /// Handles the query to retrieve a list of hotels in a city.
        /// </summary>
        /// <param name="request">The query request containing the CityId.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A list of HotelDto representing the hotels in the specified city.</returns>
        public async Task<List<HotelDto>> Handle(GetHotelsInCityQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var city = await _cityRepository.GetByIdAsync(request.CityId);

                if (city == null)
                {
                    _logger.LogWarning($"City with ID {request.CityId} not found.");
                    throw new EntityNotFoundException($"City with ID {request.CityId} not found.");
                }

                var hotels = city.Hotels;

                var hotelDtos = _mapper.Map<List<HotelDto>>(hotels);

                return hotelDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while handling GetHotelsInCityQuery for CityId: {request.CityId}");
                throw;
            }
        }
    }
}
