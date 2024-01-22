using Application.Queries.Home;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Travel_and_Accommodation_Booking_Platform.Controllers
{
    /// <summary>
    /// Controller for managing hotel-related operations.
    /// </summary>
    [ApiController]
    [Route("api/home")]
    public class HomeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="HotelController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator for handling queries and commands.</param>
        /// <param name="logger">The logger for capturing and logging information related to HotelController.</param>
        public HomeController(IMediator mediator, ILogger<HomeController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Searches for hotels based on the specified parameters.
        /// </summary>
        /// <param name="checkInDate">The check-in date for the hotel search.</param>
        /// <param name="checkOutDate">The check-out date for the hotel search.</param>
        /// <param name="city">The city for the hotel search.</param>
        /// <param name="starRate">The star rate for the hotel search.</param>
        /// <param name="sort">The sorting parameter for the hotel search.</param>
        /// <param name="numberOfRooms">The number of rooms for the hotel search.</param>
        /// <param name="adults">The number of adults for the hotel search.</param>
        /// <param name="children">The number of children for the hotel search.</param>
        /// <returns>
        /// A list of hotel data transfer objects (DTOs) matching the search criteria.
        /// </returns>
        [HttpGet("SearchHotel")]
        public async Task<IActionResult> SearchHotel(
            [FromQuery] string checkInDate,
            [FromQuery] string checkOutDate,
            [FromQuery] string city,
            [FromQuery] float starRate,
            [FromQuery] int numberOfRooms,
            [FromQuery] int adults = 1,
            [FromQuery] int children = 1)
        {
            try
            {
                var query = new SearchHotelQuery
                {
                    CheckInDate = checkInDate,
                    CheckOutDate = checkOutDate,
                    City = city,
                    StarRate = starRate,
                    NumberOfRooms = numberOfRooms,
                    AdultsCapacity = adults,
                    ChildrenCapacity = children
                };

                var hotels = await _mediator.Send(query);

                if (hotels == null || hotels.Count == 0)
                {
                    // Status Code: 404 - Not Found
                    return NotFound("No hotels found matching the search criteria.");
                }

                // Status Code: 200 - OK
                return Ok(hotels);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in SearchHotelQuery: {ex.Message}");

                // Status Code: 500 - Internal Server Error
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
    }
}
