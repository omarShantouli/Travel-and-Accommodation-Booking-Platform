using Application.Commands.Rooms_Commands;
using Application.DTOs;
using Application.Queries.Rooms_Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Travel_and_Accommodation_Booking_Platform.Controllers
{
    /// <summary>
    /// Controller for managing room-related operations.
    /// </summary>
   // [Authorize]
    [ApiController]
    [Route("api/rooms")]
    public class RoomController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<RoomController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoomController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator for handling queries and commands.</param>
        /// <param name="logger">The logger for capturing and logging information related to RoomController.</param>
        public RoomController(IMediator mediator, ILogger<RoomController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves a list of all rooms.
        /// </summary>
        /// <returns>
        /// A list of room data transfer objects (DTOs).
        /// </returns>
        [HttpGet("GetAllRooms")]
        public IActionResult GetAllRooms()
        {
            try
            {
                var query = new GetAllRoomsQuery();
                var rooms = _mediator.Send(query);

                if (rooms == null)
                {
                    // Status Code: 404 - Not Found
                    return NotFound("No rooms found!");
                }

                // Status Code: 200 - OK
                return Ok(rooms);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error in GetAllRoomsQuery: {ex.Message}");

                // Status Code: 500 - Internal Server Error
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        /// <summary>
        /// Retrieves a specific room by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the room.</param>
        /// <returns>
        /// A room data transfer object (DTO).
        /// </returns>
        [HttpGet("GetRoomById/{id}")]
        public async Task<IActionResult> GetRoomById(Guid id)
        {
            try
            {
                var query = new GetRoomByIdQuery { RoomId = id };
                var room = await _mediator.Send(query);

                if (room == null)
                {
                    // Status Code: 404 - Not Found
                    return NotFound("No room found for the given id.");
                }

                // Status Code: 200 - OK
                return Ok(room);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetRoomByIdQuery for RoomId: {id}");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        /// <summary>
        /// Creates a new room.
        /// </summary>
        /// <param name="room">The room data to be created.</param>
        /// <returns>
        /// No content with a 204 No Content status upon successful creation.
        /// </returns>
        [HttpPost("CreateRoom")]
        public async Task<IActionResult> CreateRoom([FromBody] RoomDto room)
        {
            try
            {
                var command = new CreateRoomCommand { Room = room };
                await _mediator.Send(command);

                // Return 204 No Content upon successful creation.
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a room.");
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates a room with the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the room.</param>
        /// <param name="updatedRoom">The updated room information.</param>
        /// <returns>
        /// No content with a 204 No Content status upon successful update.
        /// Bad request with a 400 Bad Request status if the updated room data is null or invalid.
        /// Not found with a 404 Not Found status if the room with the specified ID is not found.
        /// Internal server error with a 500 status if an unexpected error occurs during the update.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoom(Guid id, [FromBody] RoomDto updatedRoom)
        {
            try
            {
                if (updatedRoom == null)
                {
                    return BadRequest("The updated room data is null.");
                }

                var command = new UpdateRoomCommand { RoomId = id, UpdatedRoom = updatedRoom };
                await _mediator.Send(command);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating a room with RoomId: {id}");
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Partially updates a room using JSON Patch.
        /// </summary>
        /// <param name="id">The unique identifier of the room.</param>
        /// <param name="patchDocument">The JSON Patch document containing partial updates.</param>
        /// <returns>
        /// No content with a 204 No Content status upon successful update.
        /// Bad request with a 400 Bad Request status if the patch document is null or invalid.
        /// Not found with a 404 Not Found status if the room with the specified ID is not found.
        /// </returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchRoom(Guid id, JsonPatchDocument<RoomDto> patchDocument)
        {
            try
            {
                if (patchDocument == null)
                {
                    return BadRequest("The patch document is null.");
                }

                var command = new PatchRoomCommand { RoomId = id, patchDocument = patchDocument };
                await _mediator.Send(command);
                // Return 204 No Content upon successful update.
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while patching a room with RoomId: {id}");
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a room with the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the room to delete.</param>
        /// <returns>
        /// No content with a 204 No Content status upon successful deletion.
        /// Not found with a 404 Not Found status if the room with the specified ID is not found.
        /// Internal server error with a 500 status if an unexpected error occurs during the deletion.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(Guid id)
        {
            try
            {
                _logger.LogInformation($"Deleting Room with RoomId: {id}");

                var command = new DeleteRoomCommand { RoomId = id };
                await _mediator.Send(command);

                _logger.LogInformation($"Room with RoomId {id} successfully deleted.");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting a room with RoomId: {id}");
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a list of images associated with a specific room.
        /// </summary>
        /// <param name="id">The unique identifier of the room.</param>
        /// <returns>
        /// A collection of image data transfer objects (DTOs).
        /// </returns>
        [HttpGet("GetImagesOfRoom/{id}")]
        public async Task<IActionResult> GetImagesOfRoom(Guid id)
        {
            try
            {
                var query = new GetImagesOfRoomQuery { RoomId = id };
                var result = await _mediator.Send(query);

                // 200 OK status.
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting images of a room with RoomId: {id}");
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Adds a new image to a specific room.
        /// </summary>
        /// <param name="id">The unique identifier of the room.</param>
        /// <param name="image">The image data to be added.</param>
        /// <returns>
        /// No content with a 204 No Content status upon successful addition.
        /// </returns>
        [HttpPost("AddImageToRoom/{id}")]
        public async Task<IActionResult> AddImageToRoom(Guid id, [FromBody] ImageDto image)
        {
            try
            {
                var command = new AddImageToRoomCommand { RoomId = id, Image = image };
                await _mediator.Send(command);

                // Return 204 No Content upon successful addition.
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while adding an image to a room with RoomId: {id}");
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes an image from a specific room.
        /// </summary>
        /// <param name="roomId">The unique identifier of the room.</param>
        /// <param name="imageId">The unique identifier of the image to be deleted.</param>
        /// <returns>
        /// No content with a 204 No Content status upon successful deletion.
        /// </returns>
        [HttpDelete("DeleteImageFromRoom/{roomId}/{imageId}")]
        public async Task<IActionResult> DeleteImageFromRoom(Guid roomId, Guid imageId)
        {
            try
            {
                var command = new DeleteImageFromRoomCommand { RoomId = roomId, ImageId = imageId };
                await _mediator.Send(command);

                // Return 204 No Content upon successful deletion.
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting an image from a room with RoomId: {roomId}");
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
