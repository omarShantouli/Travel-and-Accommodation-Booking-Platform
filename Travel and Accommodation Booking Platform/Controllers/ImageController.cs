using Application.Commands.Image_Commands;
using Application.DTOs;
using Application.Queries.Image_Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Travel_and_Accommodation_Booking_Platform.Controllers
{
    /// <summary>
    /// Controller for managing image-related operations.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/images")]
    public class ImageController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ImageController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator for handling queries and commands.</param>
        /// <param name="logger">The logger for capturing and logging information related to ImageController.</param>
        public ImageController(IMediator mediator, ILogger<ImageController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves a list of all images.
        /// </summary>
        /// <returns>
        /// A list of image data transfer objects (DTOs).
        /// </returns>
        [HttpGet("GetAllImages")]
        public IActionResult GetAllImages()
        {
            try
            {
                var query = new GetAllImagesQuery();
                var images = _mediator.Send(query);

                if (images == null)
                {
                    // Status Code: 404 - Not Found
                    return NotFound("No images found!");
                }

                // Status Code: 200 - OK
                return Ok(images);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it based on your application's requirements
                _logger.LogInformation($"Error in GetAllImagesQuery: {ex.Message}");

                // Status Code: 500 - Internal Server Error
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        /// <summary>
        /// Creates a new image.
        /// </summary>
        /// <param name="image">The image data to be created.</param>
        /// <returns>
        /// No content with a 204 No Content status upon successful creation.
        /// </returns>
        [HttpPost("CreateImage")]
        public async Task<IActionResult> CreateImage([FromBody] ImageDto image)
        {
            try
            {
                var command = new CreateImageCommand { Image = image };
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
        /// Retrieves a specific image by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the image.</param>
        /// <returns>
        /// An image data transfer object (DTO).
        /// </returns>
        [HttpGet("GetImageById/{id}")]
        public async Task<IActionResult> GetImageById(Guid id)
        {
            try
            {
                var query = new GetImageByIdQuery { ImageId = id };
                var image = await _mediator.Send(query);

                if (image == null)
                {
                    // Status Code: 404 - Not Found
                    return NotFound("No image found for the given id.");
                }

                // Status Code: 200 - OK
                return Ok(image);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it based on your application's requirements
                _logger.LogInformation($"Error in GetImageByIdQuery: {ex.Message}");

                // Status Code: 500 - Internal Server Error
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        /// <summary>
        /// Updates an image with the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the image.</param>
        /// <param name="updatedImage">The updated image information.</param>
        /// <returns>
        /// No content with a 204 No Content status upon successful update.
        /// Bad request with a 400 Bad Request status if the updated image data is null or invalid.
        /// Not found with a 404 Not Found status if the image with the specified ID is not found.
        /// Internal server error with a 500 status if an unexpected error occurs during the update.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateImage(Guid id, [FromBody] ImageDto updatedImage)
        {
            try
            {
                if (updatedImage == null)
                {
                    return BadRequest("The updated image data is null.");
                }

                var command = new UpdateImageCommand { ImageId = id, UpdatedImage = updatedImage };
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
        /// Partially updates an image using JSON Patch.
        /// </summary>
        /// <param name="id">The unique identifier of the image.</param>
        /// <param name="patchDocument">The JSON Patch document containing partial updates.</param>
        /// <returns>
        /// No content with a 204 No Content status upon successful update.
        /// Bad request with a 400 Bad Request status if the patch document is null or invalid.
        /// Not found with a 404 Not Found status if the image with the specified ID is not found.
        /// </returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchImage(Guid id, JsonPatchDocument<ImageDto> patchDocument)
        {
            try
            {
                if (patchDocument == null)
                {
                    return BadRequest("The patch document is null.");
                }

                var command = new PatchImageCommand { ImageId = id, patchDocument = patchDocument };
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
        /// Deletes an image with the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the image to delete.</param>
        /// <returns>
        /// No content with a 204 No Content status upon successful deletion.
        /// Not found with a 404 Not Found status if the image with the specified ID is not found.
        /// Internal server error with a 500 status if an unexpected error occurs during the deletion.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage(Guid id)
        {
            try
            {
                var command = new DeleteImageCommand { ImageId = id };
                await _mediator.Send(command);

                // Return 204 No Content upon successful deletion.
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed.
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
