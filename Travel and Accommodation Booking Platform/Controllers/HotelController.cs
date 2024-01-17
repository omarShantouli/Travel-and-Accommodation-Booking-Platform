using Application.Commands;
using Application.Commands.Hotel_Commands;
using Application.DTOs;
using Application.Queries;
using Application.Queries.Hotels_Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Travel_and_Accommodation_Booking_Platform.Controllers
{
    /// <summary>
    /// Controller for managing hotel-related operations.
    /// </summary>
   // [Authorize]
    [ApiController]
    [Route("api/hotels")]
    public class HotelController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<HotelController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="HotelController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator for handling queries and commands.</param>
        /// <param name="logger">The logger for capturing and logging information related to HotelController.</param>
        public HotelController(IMediator mediator, ILogger<HotelController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves a list of all hotels.
        /// </summary>
        /// <returns>
        /// A list of hotel data transfer objects (DTOs).
        /// </returns>
        [HttpGet("GetAllHotels")]
        public IActionResult GetAllHotels()
        {
            try
            {
                var query = new GetAllHotelsQuery();
                var hotels = _mediator.Send(query);

                if (hotels == null)
                {
                    // Status Code: 404 - Not Found
                    return NotFound("No hotels found!");
                }

                // Status Code: 200 - OK
                return Ok(hotels);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it based on your application's requirements
                _logger.LogInformation($"Error in GetAllHotelsQuery: {ex.Message}");

                // Status Code: 500 - Internal Server Error
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }


        /// <summary>
        /// Retrieves a specific hotel by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the hotel.</param>
        /// <returns>
        /// A hotel data transfer object (DTO).
        /// </returns>
        [HttpGet("GetHotelById/{id}")]
        public async Task<IActionResult> GetHotelById(Guid id)
        {
            try
            {
                var query = new GetHotelByIdQuery { HotelId = id };
                var hotel = await _mediator.Send(query);

                if (hotel == null)
                {
                    // Status Code: 404 - Not Found
                    return NotFound("No hotel found for the given id.");
                }

                // Status Code: 200 - OK
                return Ok(hotel);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error in GetHotelByIdQuery: {ex.Message}");

                // Status Code: 500 - Internal Server Error
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        /// <summary>
        /// Creates a new hotel.
        /// </summary>
        /// <param name="hotel">The hotel data to be created.</param>
        /// <returns>
        /// No content with a 204 No Content status upon successful creation.
        /// </returns>
        [HttpPost("CreateHotel")]
        public async Task<IActionResult> CreateHotel([FromBody] HotelDto hotel)
        {
            try
            {
                var command = new CreateHotelCommand { Hotel = hotel };
                await _mediator.Send(command);

                // Return 204 No Content upon successful creation.
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed.
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a hotel with the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the hotel to delete.</param>
        /// <returns>
        /// No content with a 204 No Content status upon successful deletion.
        /// Not found with a 404 Not Found status if the hotel with the specified ID is not found.
        /// Internal server error with a 500 status if an unexpected error occurs during the deletion.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(Guid id)
        {
            try
            {
                var command = new DeleteHotelCommand { HotelId = id };
                await _mediator.Send(command);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        /// <summary>
        /// Partially updates a hotel using JSON Patch.
        /// </summary>
        /// <param name="id">The unique identifier of the hotel.</param>
        /// <param name="patchDocument">The JSON Patch document containing partial updates.</param>
        /// <returns>
        /// No content with a 204 No Content status upon successful update.
        /// Bad request with a 400 Bad Request status if the patch document is null or invalid.
        /// Not found with a 404 Not Found status if the hotel with the specified ID is not found.
        /// </returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchHotel(Guid id, JsonPatchDocument<HotelDto> patchDocument)
        {
            try
            {
                if (patchDocument == null)
                {
                    return BadRequest("The patch document is null.");
                }

                var command = new PatchHotelCommand { HotelId = id, patchDocument = patchDocument };
                await _mediator.Send(command);
                // Return 204 No Content upon successful update.
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed.
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates a hotel with the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the hotel.</param>
        /// <param name="updatedHotel">The updated hotel information.</param>
        /// <returns>
        /// No content with a 204 No Content status upon successful update.
        /// Bad request with a 400 Bad Request status if the updated hotel data is null or invalid.
        /// Not found with a 404 Not Found status if the hotel with the specified ID is not found.
        /// Internal server error with a 500 status if an unexpected error occurs during the update.
        /// </returns>
        [HttpPut("UpdateHotel/{id}")]
        public async Task<IActionResult> UpdateHotel(Guid id, [FromBody] HotelDto updatedHotel)
        {
            try
            {
                if (updatedHotel == null)
                {
                    return BadRequest("The updated hotel data is null.");
                }

                var command = new UpdateHotelCommand { HotelId = id, UpdatedHotel = updatedHotel };
                await _mediator.Send(command);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        /// <summary>
        /// Retrieves a list of available rooms in a specific hotel.
        /// </summary>
        /// <param name="id">The unique identifier of the hotel.</param>
        /// <returns>
        /// <see cref="IActionResult"/> with HTTP status code:
        /// - 200 (OK) if available rooms are found.
        /// - 404 (Not Found) if no available rooms are found for the given hotel ID.
        /// - 500 (Internal Server Error) in case of unexpected errors.
        /// </returns>
        [HttpGet("GetAvailableRoomsInHotel/{id}")]
        public async Task<IActionResult> GetAvailableRoomsInHotel(Guid id)
        {
            try
            {
                var query = new GetAvailableRoomsInHotelQuery { HotelId = id };
                var rooms = await _mediator.Send(query);

                if (rooms == null)
                {
                    return NotFound("No available rooms found for the given hotel id.");
                }

                return Ok(rooms);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error in GetAvailableRoomsInHotelQuery: {ex.Message}");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        /// <summary>
        /// Retrieves a list of rooms in a specific hotel.
        /// </summary>
        /// <param name="id">The unique identifier of the hotel.</param>
        /// <returns>
        /// <see cref="IActionResult"/> with HTTP status code:
        /// - 200 (OK) if rooms are found.
        /// - 404 (Not Found) if no rooms are found for the given hotel ID.
        /// - 500 (Internal Server Error) in case of unexpected errors.
        /// </returns>
        [HttpGet("GetRoomsInHotel/{id}")]
        public async Task<IActionResult> GetRoomsInHotel(Guid id)
        {
            try
            {
                var query = new GetRoomsInHotelQuery { HotelId = id };
                var rooms = await _mediator.Send(query);

                if (rooms == null)
                {
                    return NotFound("No rooms found for the given hotel id.");
                }

                return Ok(rooms);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error in GetRoomsInHotelQuery: {ex.Message}");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        /// <summary>
        /// Adds a new room to a specific hotel.
        /// </summary>
        /// <param name="id">The unique identifier of the hotel.</param>
        /// <param name="room">The room data to be added.</param>
        /// <returns>
        /// No content with a 204 No Content status upon successful addition.
        /// </returns>
        [HttpPost("AddRoomInHotel/{id}")]
        public async Task<IActionResult> AddRoomInHotel(Guid id, [FromBody] RoomDto room)
        {
            try
            {
                var command = new AddRoomInHotelCommand { HotelId = id, Room = room };
                await _mediator.Send(command);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a room from a specific hotel.
        /// </summary>
        /// <param name="hotelId">The unique identifier of the hotel.</param>
        /// <param name="roomId">The unique identifier of the room to be deleted.</param>
        /// <returns>
        /// No content with a 204 No Content status upon successful deletion.
        /// </returns>
        [HttpDelete("DeleteRoomFromHotel/{hotelId}/{roomId}")]
        public async Task<IActionResult> DeleteRoomFromHotel(Guid hotelId, Guid roomId)
        {
            try
            {
                var command = new DeleteRoomFromHotelCommand { HotelId = hotelId, RoomId = roomId };
                await _mediator.Send(command);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a list of images associated with a specific hotel.
        /// </summary>
        /// <param name="id">The unique identifier of the hotel.</param>
        /// <returns>
        /// A collection of image data transfer objects (DTOs).
        /// </returns>
        [HttpGet("GetImagesOfHotel/{id}")]
        public async Task<IActionResult> GetImagesOfHotel(Guid id)
        {
            try
            {
                var query = new GetImagesOfHotelQuery { HotelId = id };
                var images = await _mediator.Send(query);

                if (images == null)
                {
                    return NotFound("No images found for the given hotel id.");
                }

                return Ok(images);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Adds a new image to a specific hotel.
        /// </summary>
        /// <param name="id">The unique identifier of the hotel.</param>
        /// <param name="image">The image data to be added.</param>
        /// <returns>
        /// No content with a 204 No Content status upon successful addition.
        /// </returns>
        [HttpPost("AddImageToHotel/{id}")]
        public async Task<IActionResult> AddImageToHotel(Guid id, [FromBody] ImageDto image)
        {
            try
            {
                var command = new AddImageToHotelCommand { HotelId = id, Image = image };
                await _mediator.Send(command);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes an image from a specific hotel.
        /// </summary>
        /// <param name="hotelId">The unique identifier of the hotel.</param>
        /// <param name="imageId">The unique identifier of the image to be deleted.</param>
        /// <returns>
        /// No content with a 204 No Content status upon successful deletion.
        /// </returns>
        [HttpDelete("DeleteImageFromHotel/{hotelId}/{imageId}")]
        public async Task<IActionResult> DeleteImageFromHotel(Guid hotelId, Guid imageId)
        {
            try
            {
                var command = new DeleteImageFromHotelCommand { HotelId = hotelId, ImageId = imageId };
                await _mediator.Send(command);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

    }
}