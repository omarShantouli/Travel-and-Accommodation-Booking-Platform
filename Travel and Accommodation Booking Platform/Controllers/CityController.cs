using Application.Commands;
using Application.DTOs;
using Application.Queries;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using static Domain.Interfaces.IRepository;

namespace Travel_and_Accommodation_Booking_Platform.Controllers
{
    /// <summary>
    /// Controller for managing city-related operations.
    /// </summary>
    //[Authorize]
    [ApiController]
    [Route("api/cities")]
    public class CityController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IRepository<City> _cityRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CityController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator for handling queries and commands.</param>
        /// <param name="cityRepository">The repository for city entities.</param>
        /// <param name="mapper">The mapper for mapping between DTOs and entities.</param>
        public CityController(IMediator mediator, IRepository<City> cityRepository, IMapper mapper)
        {
            _mediator = mediator;
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves a list of all cities.
        /// </summary>
        /// <returns>
        /// A list of city data transfer objects (DTOs).
        /// </returns>
        [HttpGet("GetAllCities")]
        public IActionResult GetAllCities()
        {
            try
            {
                var cities = _cityRepository.GetAll();
                var result = _mapper.Map<List<CityDto>>(cities);

                // If cities are found, return them with a 200 OK status.
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed.
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Creates a new city.
        /// </summary>
        /// <param name="city">The city data to be created.</param>
        /// <returns>
        /// No content with a 204 No Content status upon successful creation.
        /// </returns>
        [HttpPost("CreateCity")]
        public async Task<IActionResult> CreateCity([FromBody] CityDto city)
        {
            try
            {
                var cityToAdd = _mapper.Map<City>(city);
                await _cityRepository.CreateAsync(cityToAdd);

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
        /// Retrieves a specific city by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the city.</param>
        /// <returns>
        /// A city data transfer object (DTO).
        /// </returns>
        [HttpGet("GetCityById/{id}")]
        public async Task<IActionResult> GetCityById(Guid id)
        {
            try
            {
                var city = await _cityRepository.GetByIdAsync(id);

                // If the city is found, return it with a 200 OK status.
                if (city != null)
                {
                    var result = _mapper.Map<CityDto>(city);
                    return Ok(result);
                }

                // If the city is not found, return a 404 Not Found status.
                return NotFound($"City with ID {id} not found.");
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed.
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates a city with the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the city.</param>
        /// <param name="updatedCity">The updated city information.</param>
        /// <returns>
        /// No content with a 204 No Content status upon successful update.
        /// Bad request with a 400 Bad Request status if the updated city data is null or invalid.
        /// Not found with a 404 Not Found status if the city with the specified ID is not found.
        /// Internal server error with a 500 status if an unexpected error occurs during the update.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCity(Guid id, [FromBody] CityDto updatedCity)
        {
            try
            {
                if (updatedCity == null)
                {
                    return BadRequest("The updated city data is null.");
                }

                var existingCity = await _cityRepository.GetByIdAsync(id);

                if (existingCity == null)
                {
                    return NotFound($"City with ID {id} not found.");
                }

                _mapper.Map(updatedCity, existingCity);

                await _cityRepository.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Partially updates a city using JSON Patch.
        /// </summary>
        /// <param name="id">The unique identifier of the city.</param>
        /// <param name="patchDocument">The JSON Patch document containing partial updates.</param>
        /// <returns>
        /// No content with a 204 No Content status upon successful update.
        /// Bad request with a 400 Bad Request status if the patch document is null or invalid.
        /// Not found with a 404 Not Found status if the city with the specified ID is not found.
        /// </returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchCity(Guid id, JsonPatchDocument<CityDto> patchDocument)
        {
            try
            {
                if (patchDocument == null)
                {
                    return BadRequest("The patch document is null.");
                }

                var city = await _cityRepository.GetByIdAsync(id);

                if (city == null)
                {
                    return NotFound($"City with ID {id} not found.");
                }

                var cityDto = new CityDto
                {
                    Id = city.Id,
                    Name = city.Name,
                    CountryName = city.CountryName,
                    PostOffice = city.PostOffice,
                    CountryCode = city.CountryCode
                };

                // Apply the patch document to the DTO
                patchDocument.ApplyTo(cityDto, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Update the entity with the changes
                city.Name = cityDto.Name;
                city.CountryName = cityDto.CountryName;
                city.PostOffice = cityDto.PostOffice;
                city.CountryCode = cityDto.CountryCode;

                // Save changes
                await _cityRepository.SaveChangesAsync();

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
        /// Deletes a city with the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the city to delete.</param>
        /// <returns>
        /// No content with a 204 No Content status upon successful deletion.
        /// Not found with a 404 Not Found status if the city with the specified ID is not found.
        /// Internal server error with a 500 status if an unexpected error occurs during the deletion.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(Guid id)
        {
            try
            {
                var existingCity = await _cityRepository.GetByIdAsync(id);

                if (existingCity == null)
                {
                    return NotFound($"City with ID {id} not found.");
                }

                await _cityRepository.DeleteAsync(existingCity.Id);

                await _cityRepository.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a list of hotels based on the specified city ID.
        /// </summary>
        /// <param name="id">The unique identifier of the city.</param>
        /// <returns>
        /// <see cref="IActionResult"/> with HTTP status code:
        /// - 200 (OK) if hotels are found.
        /// - 404 (Not Found) if no hotels are found for the given city ID.
        /// - 500 (Internal Server Error) in case of unexpected errors.
        /// </returns>
        [HttpGet("GetHotelsByCityId/{id}")]
        public async Task<IActionResult> GetHotelsByCityId(Guid id)
        {
            try
            {
                var query = new GetHotelsInCityQuery { CityId = id };
                var hotels = await _mediator.Send(query);

                if (hotels == null)
                {
                    // Status Code: 404 - Not Found
                    return NotFound("No hotels found for the given city id.");
                }

                // Status Code: 200 - OK
                return Ok(hotels);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it based on your application's requirements
                Console.WriteLine($"Error in GetHotelsByCityId: {ex.Message}");

                // Status Code: 500 - Internal Server Error
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        /// <summary>
        /// Adds a new hotel to a specific city.
        /// </summary>
        /// <param name="id">The unique identifier of the city.</param>
        /// <param name="hotel">The hotel data to be added.</param>
        /// <returns>
        /// No content with a 204 No Content status upon successful addition.
        /// </returns>
        [HttpPost("AddHotelInCity/{id}")]
        public async Task<IActionResult> AddHotelInCity(Guid id, [FromBody] HotelDto hotel)
        {
            try
            {
                var command = new AddHotelInCityCommand { CityId = id, Hotel = hotel };
                await _mediator.Send(command);

                // Return 204 No Content upon successful addition.
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed.
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a hotel from a specific city.
        /// </summary>
        /// <param name="cityId">The unique identifier of the city.</param>
        /// <param name="hotelId">The unique identifier of the hotel to be deleted.</param>
        /// <returns>
        /// No content with a 204 No Content status upon successful deletion.
        /// </returns>
        [HttpDelete("DeleteHotelFromCity/{cityId}/{hotelId}")]
        public async Task<IActionResult> DeleteHotelFromCity(Guid cityId, Guid hotelId)
        {
            try
            {
                var command = new DeleteHotelFromCityCommand { CityId = cityId, HotelId = hotelId };
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

        /// <summary>
        /// Retrieves a list of images associated with a specific city.
        /// </summary>
        /// <param name="id">The unique identifier of the city.</param>
        /// <returns>
        /// A collection of image data transfer objects (DTOs).
        /// </returns>
        [HttpGet("GetImagesByCityId/{id}")]
        public async Task<IActionResult> GetImagesByCityId(Guid id)
        {
            try
            {
                var query = new GetImagesOfCityQuery { CityId = id };
                var result = await _mediator.Send(query);

                // If images are found, return them with a 200 OK status.
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed.
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Adds a new image to a specific city.
        /// </summary>
        /// <param name="id">The unique identifier of the city.</param>
        /// <param name="image">The image data to be added.</param>
        /// <returns>
        /// No content with a 204 No Content status upon successful addition.
        /// </returns>
        [HttpPost("AddImageToCity/{id}")]
        public async Task<IActionResult> AddImageToCity(Guid id, [FromBody] ImageDto image)
        {
            try
            {
                var command = new AddImageToCityCommand { CityId = id, Image = image };
                await _mediator.Send(command);

                // Return 204 No Content upon successful addition.
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed.
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes an image from a specific city.
        /// </summary>
        /// <param name="cityId">The unique identifier of the city.</param>
        /// <param name="imageId">The unique identifier of the image to be deleted.</param>
        /// <returns>
        /// No content with a 204 No Content status upon successful deletion.
        /// </returns>
        [HttpDelete("DeleteImageFromCity/{cityId}/{imageId}")]
        public async Task<IActionResult> DeleteImageFromCity(Guid cityId, Guid imageId)
        {
            try
            {
                var command = new DeleteImageFromCityCommand { CityId = cityId, ImageId = imageId };
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
