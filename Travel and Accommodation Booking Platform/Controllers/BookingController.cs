using Application.Commands.Booking_Commands;
using Application.DTOs;
using Application.Queries.Booking_Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Travel_and_Accommodation_Booking_Platform.Controllers
{
    /// <summary>
    /// Controller for managing booking-related operations.
    /// </summary>
   // [Authorize]
    [ApiController]
    [Route("api/bookings")]
    public class BookingController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BookingController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookingController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator for handling queries and commands.</param>
        /// <param name="logger">The logger for capturing and logging information related to BookingController.</param>
        public BookingController(IMediator mediator, ILogger<BookingController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new booking.
        /// </summary>
        /// <param name="booking">The booking data to be created.</param>
        /// <returns>
        /// No content with a 204 No Content status upon successful creation.
        /// </returns>
        [HttpPost("CreateBooking")]
        public async Task<IActionResult> CreateBooking([FromBody] BookingDto booking)
        {
            try
            {
                var command = new CreateBookingCommand { Booking = booking };
                await _mediator.Send(command);

                // Return 204 No Content upon successful creation.
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a specific booking by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the booking.</param>
        /// <returns>
        /// A booking data transfer object (DTO).
        /// </returns>
        [HttpGet("GetBookingById/{id}")]
        public async Task<IActionResult> GetBookingById(Guid id)
        {
            try
            {
                var query = new GetBookingByIdQuery { BookingId = id };
                var booking = await _mediator.Send(query);

                if (booking == null)
                {
                    // Status Code: 404 - Not Found
                    return NotFound("No booking found for the given id.");
                }

                // Status Code: 200 - OK
                return Ok(booking);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error in GetBookingByIdQuery: {ex.Message}");

                // Status Code: 500 - Internal Server Error
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
    }
}
