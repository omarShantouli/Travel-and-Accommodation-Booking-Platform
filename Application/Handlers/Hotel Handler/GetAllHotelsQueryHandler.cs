using Application.DTOs;
using Application.Queries.Hotels_Queries;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers.Hotel_Handler
{
    /// <summary>
    /// Handles the query to get all hotels.
    /// </summary>
    public class GetAllHotelsQueryHandler : IRequestHandler<GetAllHotelsQuery, List<HotelDto>>
    {
        private readonly IRepository<Hotels> _hotelRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllHotelsQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllHotelsQueryHandler"/> class.
        /// </summary>
        /// <param name="hotelRepository">The repository for hotel entities.</param>
        /// <param name="mapper">The AutoMapper for mapping entities to DTOs.</param>
        /// <param name="logger">The logger for capturing and logging information related to GetAllHotelsQueryHandler.</param>
        public GetAllHotelsQueryHandler(IRepository<Hotels> hotelRepository, IMapper mapper, ILogger<GetAllHotelsQueryHandler> logger)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Handles the query to get all hotels.
        /// </summary>
        /// <param name="request">The query request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of HotelDto representing all hotels.</returns>
        public async Task<List<HotelDto>> Handle(GetAllHotelsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Handling GetAllHotelsQuery.");

                var hotels = _hotelRepository.GetAll();

                if (hotels == null)
                {
                    _logger.LogWarning("No hotels found.");
                    throw new EntityNotFoundException("There are no hotels found.");
                }

                var hotelsDto = _mapper.Map<List<HotelDto>>(hotels);

                _logger.LogInformation("GetAllHotelsQuery handled successfully.");

                return hotelsDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while handling GetAllHotelsQuery.");
                throw;
            }
        }
    }
}
