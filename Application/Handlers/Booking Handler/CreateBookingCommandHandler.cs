using Application.Commands.Booking_Commands;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers.Booking_Handler
{
    /// <summary>
    /// Handles the command to create a new booking.
    /// </summary>
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand>
    {
        private readonly IRepository<Bookings> _bookingRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateBookingCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateBookingCommandHandler"/> class.
        /// </summary>
        /// <param name="bookingRepository">The repository for booking entities.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing and logging information related to CreateBookingCommandHandler.</param>
        public CreateBookingCommandHandler(IRepository<Bookings> bookingRepository, IMapper mapper, ILogger<CreateBookingCommandHandler> logger)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Handles the command to create a new booking.
        /// </summary>
        /// <param name="request">The command request containing booking information.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public async Task Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Booking == null)
                {
                    _logger.LogError("Booking object is null.");
                    throw new EntityNotFoundException("Booking object is null.");
                }

                if (await HasBookingConflict(request.Booking))
                {
                    _logger.LogWarning("Booking conflict detected. The new booking overlaps with existing bookings.");
                    throw new BookingConflictException("Booking conflict detected. The new booking overlaps with existing bookings.");
                }

                var bookingToAdd = _mapper.Map<Bookings>(request.Booking);
                await _bookingRepository.CreateAsync(bookingToAdd);
                await _bookingRepository.SaveChangesAsync();

                _logger.LogInformation($"Booking created successfully. BookingId: {bookingToAdd.Id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while handling CreateBookingCommand.");
                throw;
            }
        }

        /// <summary>
        /// Checks if the new booking conflicts with existing bookings.
        /// </summary>
        /// <param name="newBooking">The new booking to check for conflicts.</param>
        /// <returns>True if there is a conflict, otherwise false.</returns>
        private async Task<bool> HasBookingConflict(BookingDto newBooking)
        {
            var existingBookings = _bookingRepository.GetAll(); 

            return existingBookings.Any(existingBooking =>
                newBooking.CheckInDate < existingBooking.CheckOutDate &&
                newBooking.CheckOutDate > existingBooking.CheckInDate);
        }
    }
}
