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
    /// Handles the query to get a hotel by its ID.
    /// </summary>
    public class GetHotelByIdQueryHandler : IRequestHandler<GetHotelByIdQuery, HotelDto>
    {
        private readonly IRepository<Hotels> _hotelRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetHotelByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetHotelByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="hotelRepository">The repository for hotel entities.</param>
        /// <param name="mapper">The AutoMapper for mapping entities to DTOs.</param>
        /// <param name="logger">The logger for capturing and logging information related to GetHotelByIdQueryHandler.</param>
        public GetHotelByIdQueryHandler(IRepository<Hotels> hotelRepository, IMapper mapper, ILogger<GetHotelByIdQueryHandler> logger)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Handles the query to get a hotel by its ID.
        /// </summary>
        /// <param name="request">The query request containing the HotelId.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The HotelDto representing the hotel with the specified ID.</returns>
        public async Task<HotelDto> Handle(GetHotelByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Handling GetHotelByIdQuery for HotelId: {request.HotelId}");

                var hotel = await _hotelRepository.GetByIdAsync(request.HotelId);

                if (hotel == null)
                {
                    _logger.LogWarning($"Hotel with ID {request.HotelId} not found.");
                    throw new EntityNotFoundException($"Hotel with ID {request.HotelId} not found.");
                }

                var hotelDto = _mapper.Map<HotelDto>(hotel);

                _logger.LogInformation($"GetHotelByIdQuery handled successfully for HotelId: {request.HotelId}");

                return hotelDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while handling GetHotelByIdQuery for HotelId: {request.HotelId}");
                throw;
            }
        }
    }
}
