using Application.Commands.RoomType_Commands;
using Application.DTOs;
using Application.Queries.RoomType_Queries;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Travel_and_Accommodation_Booking_Platform.Controllers
{
    /// <summary>
    /// Controller for managing room type-related operations.
    /// </summary>
    //[Authorize]
    [ApiController]
    [Route("api/roomtypes")]
    public class RoomTypeController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<RoomTypeController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoomTypeController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator for handling queries and commands.</param>
        /// <param name="logger">The logger for capturing and logging information related to RoomTypeController.</param>
        public RoomTypeController(IMediator mediator, ILogger<RoomTypeController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves a list of all room types.
        /// </summary>
        /// <returns>
        /// A list of room type data transfer objects (DTOs).
        /// </returns>
        [HttpGet("GetAllRoomTypes")]
        public IActionResult GetAllRoomTypes()
        {
            try
            {
                var query = new GetAllRoomTypesQuery();
                var roomTypes = _mediator.Send(query);

                if (roomTypes == null)
                {
                    // Status Code: 404 - Not Found
                    return NotFound("No room types found!");
                }

                // Status Code: 200 - OK
                return Ok(roomTypes);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error in GetAllRoomTypesQuery: {ex.Message}");

                // Status Code: 500 - Internal Server Error
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        /// <summary>
        /// Creates a new room type.
        /// </summary>
        /// <param name="roomType">The room type data to be created.</param>
        /// <returns>
        /// No content with a 204 No Content status upon successful creation.
        /// </returns>
        [HttpPost("CreateRoomType")]
        public async Task<IActionResult> CreateRoomType([FromBody] RoomTypeDto roomType)
        {
            try
            {
                var command = new CreateRoomTypeCommand { RoomType = roomType };
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
        /// Retrieves a specific room type by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the room type.</param>
        /// <returns>
        /// A room type data transfer object (DTO).
        /// </returns>
        [HttpGet("GetRoomTypeById/{id}")]
        public async Task<IActionResult> GetRoomTypeById(Guid id)
        {
            try
            {
                var query = new GetRoomTypeByIdQuery { RoomTypeId = id };
                var roomType = await _mediator.Send(query);

                if (roomType == null)
                {
                    // Status Code: 404 - Not Found
                    return NotFound("No room type found for the given id.");
                }

                // Status Code: 200 - OK
                return Ok(roomType);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error in GetRoomTypeByIdQuery: {ex.Message}");

                // Status Code: 500 - Internal Server Error
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        /// <summary>
        /// Updates a room type with the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the room type.</param>
        /// <param name="updatedRoomType">The updated room type information.</param>
        /// <returns>
        /// No content with a 204 No Content status upon successful update.
        /// Bad request with a 400 Bad Request status if the updated room type data is null or invalid.
        /// Not found with a 404 Not Found status if the room type with the specified ID is not found.
        /// Internal server error with a 500 status if an unexpected error occurs during the update.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoomType(Guid id, [FromBody] RoomTypeDto updatedRoomType)
        {
            try
            {
                if (updatedRoomType == null)
                {
                    return BadRequest("The updated room type data is null.");
                }

                var command = new UpdateRoomTypeCommand { RoomTypeId = id, UpdatedRoomType = updatedRoomType };
                await _mediator.Send(command);

                // Return 204 No Content upon successful update.
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Partially updates a room type using JSON Patch.
        /// </summary>
        /// <param name="id">The unique identifier of the room type.</param>
        /// <param name="patchDocument">The JSON Patch document containing partial updates.</param>
        /// <returns>
        /// No content with a 204 No Content status upon successful update.
        /// Bad request with a 400 Bad Request status if the patch document is null or invalid.
        /// Not found with a 404 Not Found status if the room type with the specified ID is not found.
        /// </returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchRoomType(Guid id, JsonPatchDocument<RoomTypeDto> patchDocument)
        {
            try
            {
                if (patchDocument == null)
                {
                    return BadRequest("The patch document is null.");
                }

                var command = new PatchRoomTypeCommand { RoomTypeId = id, patchDocument = patchDocument };
                await _mediator.Send(command);

                // Return 204 No Content upon successful update.
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a room type with the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the room type to delete.</param>
        /// <returns>
        /// No content with a 204 No Content status upon successful deletion.
        /// Not found with a 404 Not Found status if the room type with the specified ID is not found.
        /// Internal server error with a 500 status if an unexpected error occurs during the deletion.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomType(Guid id)
        {
            try
            {
                var command = new DeleteRoomTypeCommand { RoomTypeId = id };
                await _mediator.Send(command);

                // Return 204 No Content upon successful deletion.
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets a list of rooms of a specific room type.
        /// </summary>
        /// <param name="roomTypeId">The RoomTypeId for which to retrieve rooms.</param>
        /// <returns>A list of RoomDto representing the rooms of the specified room type.</returns>
        [HttpGet("GetRoomsOfRoomType/{roomTypeId}")]
        public async Task<ActionResult<List<RoomDto>>> GetRoomsOfRoomType(Guid roomTypeId)
        {
            try
            {
                var query = new GetRoomsOfRoomTypeQuery { RoomTypeId = roomTypeId };

                _logger.LogInformation($"Handling GetRoomsOfRoomType query for RoomTypeId: {roomTypeId}");

                var result = await _mediator.Send(query);

                _logger.LogInformation($"Successfully handled GetRoomsOfRoomType query for RoomTypeId: {roomTypeId}");

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while handling GetRoomsOfRoomType query for RoomTypeId: {roomTypeId}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Adds a room to a specific room type.
        /// </summary>
        /// <param name="roomTypeId">The unique identifier of the room type.</param>
        /// <param name="roomId">The unique identifier of the room.</param>
        /// <returns>Returns an action result indicating success or failure.</returns>
        [HttpPost("AddRoomToRoomType")]
        public async Task<IActionResult> AddRoomToRoomType(Guid roomTypeId, Guid roomId)
        {
            try
            {
                var command = new AddRoomToRoomTypeCommand { RoomId = roomId, RoomTypeId = roomTypeId };
                _logger.LogInformation($"Handling AddRoomToRoomTypeCommand for RoomId: {command.RoomId}," +
                    $" RoomTypeId: {command.RoomTypeId}");

                await _mediator.Send(command);

                _logger.LogInformation($"Successfully handled AddRoomToRoomTypeCommand for RoomId: {command.RoomId}," +
                    $" RoomTypeId: {command.RoomTypeId}");

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while handling AddRoomToRoomTypeCommand for RoomId: {roomId}," +
                    $" RoomTypeId: {roomTypeId}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        /// <summary>
        /// Deletes a room by RoomId and RoomTypeId.
        /// </summary>
        /// <param name="roomTypeId">The unique identifier of the room type.</param>
        /// <param name="roomId">The unique identifier of the room.</param>
        /// <returns>Returns an action result indicating success or failure.</returns>
        [HttpDelete("DeleteRoomByRoomType")]
        public async Task<IActionResult> DeleteRoomByRoomType(Guid roomTypeId, Guid roomId)
        {
            try
            {
                var command = new DeleteRoomByRoomTypeCommand { RoomId = roomId, RoomTypeId = roomTypeId };
                _logger.LogInformation($"Handling DeleteRoomByRoomTypeCommand for RoomId: {command.RoomId}," +
                    $" RoomTypeId: {command.RoomTypeId}");

                await _mediator.Send(command);

                _logger.LogInformation($"Successfully handled DeleteRoomByRoomTypeCommand for RoomId: {command.RoomId}," +
                    $" RoomTypeId: {command.RoomTypeId}");

                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while handling DeleteRoomByRoomTypeCommand: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}
