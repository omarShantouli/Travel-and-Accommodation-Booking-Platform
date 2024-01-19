using Application.DTOs;
using Application.Queries.Booking_Queries;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers.Booking_Handler
{
    /// <summary>
    /// Handles the query to get a booking by its ID.
    /// </summary>
    public class GetBookingByIdQueryHandler : IRequestHandler<GetBookingByIdQuery, BookingDto>
    {
        private readonly IRepository<Bookings> _bookingRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetBookingByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetBookingByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="bookingRepository">The repository for booking entities.</param>
        /// <param name="mapper">The AutoMapper for mapping entities to DTOs.</param>
        /// <param name="logger">The logger for capturing and logging information related to GetBookingByIdQueryHandler.</param>
        public GetBookingByIdQueryHandler(IRepository<Bookings> bookingRepository, IMapper mapper, ILogger<GetBookingByIdQueryHandler> logger)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Handles the query to get a booking by its ID.
        /// </summary>
        /// <param name="request">The query request containing the BookingId.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The BookingDto representing the booking with the specified ID.</returns>
        public async Task<BookingDto> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Handling GetBookingByIdQuery for BookingId: {request.BookingId}");

                var booking = await _bookingRepository.GetByIdAsync(request.BookingId);

                if (booking == null)
                {
                    _logger.LogWarning($"Booking with ID {request.BookingId} not found.");
                    throw new EntityNotFoundException($"Booking with ID {request.BookingId} not found.");
                }

                var bookingDto = _mapper.Map<BookingDto>(booking);

                _logger.LogInformation($"GetBookingByIdQuery handled successfully for BookingId: {request.BookingId}");

                return bookingDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while handling GetBookingByIdQuery for BookingId: {request.BookingId}");
                throw;
            }
        }
    }
}
