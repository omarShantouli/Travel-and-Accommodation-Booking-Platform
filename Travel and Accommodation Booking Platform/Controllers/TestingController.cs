using Application.Commands;
using Application.Commands.Hotel_Commands;
using Application.Commands.Rooms_Commands;
using Application.DTOs;
using Application.Queries;
using Application.Queries.Rooms_Queries;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using static Domain.Interfaces.IRepository;

namespace Travel_and_Accommodation_Booking_Platform.Controllers
{
    [ApiController]
    [Route("api/Testing")]
    public class TestingController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IRepository<City> _cityrepository;
        private readonly IMapper _mapper;
        public TestingController(IMediator mediator, IRepository<City> cityrepository, IMapper mapper)
        {
            _mediator = mediator;
            _cityrepository = cityrepository;
            _mapper = mapper;
        }

        [HttpGet("GetHotelsByCityId/{id}")] // done
        public async Task<List<HotelDto>> GetHotelsByCityId(Guid id)
        {
            var query = new GetHotelsInCityQuery { CityId = id };
            var result = await _mediator.Send(query);
            Console.WriteLine(result);
            return result;
        }

        [HttpGet("GetImagesByCityId/{id}")] // done
        public async Task<List<ImageDto>> GetImagesByCityId(Guid id)
        {
            var query = new GetImagesOfCityQuery { CityId = id };
            var result = await _mediator.Send(query);
            return result;
        }

        [HttpGet("GetRoomsByHotelId/{id}")]
        public async Task<List<RoomDto>> GetRoomsByHotelId(Guid id)
        {
            var query = new GetRoomsInHotelQuery { HotelId = id };
            var result = await _mediator.Send(query);
            return result;
        }

        [HttpGet("GetAllCities")] // done
        public List<CityDto> GetAllCities()
        {
            Console.WriteLine("omar hantouli");
            var cities = _cityrepository.GetAll();
            var result = _mapper.Map<List<CityDto>>(cities);
            return result;
        }

        [HttpGet("GetCitById/{Id}")] // done
        public async Task<CityDto> GetCitById(Guid Id)
        {
            var city = await _cityrepository.GetByIdAsync(Id);
            var result = _mapper.Map<CityDto>(city);
            return result;
        }

        [HttpGet("GetAvailableRoomsByHotelId/{id}")]
        public async Task<List<RoomDto>> GetAvailableRoomsByHotelId(Guid id)
        {
            var query = new GetAvailableRoomsInHotelQuery { HotelId = id };
            var result = await _mediator.Send(query);
            return result;
        }
        [HttpGet("GetReviewsByHotelId/{id}")]
        public async Task<List<ReviewDto>> GetReviewsByHotelId(Guid id)
        {
            var query = new GetReviewsOfHotelQuery { HotelId = id };
            var result = await _mediator.Send(query);
            return result;
        }
        [HttpGet("GetImagesOfRoomQuery/{id}")]
        public async Task<List<ImageDto>> GetImagesByRoomId(Guid id)
        {
            var query = new GetImagesOfRoomQuery { RoomId = id };
            var result = await _mediator.Send(query);
            return result;
        }

        [HttpGet("GetImagesOfHotelQuery/{id}")]
        public async Task<List<ImageDto>> GetImagesByHotelId(Guid id)
        {
            var query = new GetImagesOfHotelQuery { HotelId = id };
            var result = await _mediator.Send(query);
            return result;
        }

        [HttpPost("CreateCity")]
        public async Task AddHotelInCity([FromBody] CityDto city)
        {
            var citToAdd = _mapper.Map<City>(city);
           await _cityrepository.CreateAsync(citToAdd);

        }

        [HttpPost("AddImageToHotel/{id}")]
        public async Task AddHotelImage(Guid id, [FromBody] ImageDto image)
        {
            Console.WriteLine("===========> ", image.URL);
            var query = new AddImageToHotelCommand { HotelId = id , Image = image};
            await _mediator.Send(query);

        }

        [HttpPost("AddRoomInHotel/{id}")]
        public async Task AddRoomInHotel(Guid id, [FromBody] RoomDto room)
        {
            var query = new AddRoomInHotelCommand { HotelId = id , Room = room};
            await _mediator.Send(query);

        }

        [HttpDelete("DeleteImageFromHotel/{hotelId}/{imageId}")]
        public async Task DeleteImageFromHotel(Guid hotelId, Guid imageId)
        {
            var query = new DeleteImageFromHotelCommand { HotelId = hotelId, ImageId = imageId };
            await _mediator.Send(query);

        }

        [HttpDelete("DeleteRoomFromHotel/{hotelId}/{roomId}")]
        public async Task DeleteRoomFromHotel(Guid hotelId, Guid roomId)
        {
            var query = new DeleteRoomFromHotelCommand { HotelId = hotelId, RoomId = roomId};
            await _mediator.Send(query);

        }

        [HttpPost("AddImageToRoom/{id}")]
        public async Task AddImageToRoom(Guid id, [FromBody] ImageDto image)
        {
            var query = new AddImageToRoomCommand {RoomId = id,  Image = image};
            await _mediator.Send(query);

        }

        [HttpDelete("DeleteImageFromRoom/{roomId}/{imageId}")]
        public async Task DeleteImageFromRoom(Guid roomId, Guid imageId)
        {
            var query = new DeleteImageFromRoomCommand { RoomId = roomId,  ImageId = imageId };
            await _mediator.Send(query);

        }

        [HttpPost("AddHotelInCity/{id}")]
        public async Task AddHotelInCity(Guid id, [FromBody] HotelDto hotel)
        {
            var query = new AddHotelInCityCommand { CityId = id, Hotel = hotel };
            await _mediator.Send(query);

        }

        [HttpPost("AddImageToCity/{id}")]
        public async Task AddImageToCity(Guid id, [FromBody] ImageDto image)
        {
            var query = new AddImageToCityCommand { CityId = id, Image = image };
            await _mediator.Send(query);

        }

        [HttpDelete("DeleteHotelFromCity/{cityId}/{hotelId}")]
        public async Task DeleteHotelFromCity(Guid cityId, Guid hotelId)
        {
            var query = new DeleteHotelFromCityCommand { CityId = cityId, HotelId = hotelId };
            await _mediator.Send(query);

        }

        [HttpDelete("DeleteImageFromCity/{cityId}/{imageId}")]
        public async Task DeleteImageFromCity(Guid cityId, Guid imageId)
        {
            var query = new DeleteImageFromCityCommand { CityId = cityId, ImageId = imageId };
            await _mediator.Send(query);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchDocument"></param>
        /// <returns></returns>
        
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchCity(Guid id,  JsonPatchDocument<CityDto> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var city = await _cityrepository.GetByIdAsync(id);

            if (city == null)
            {
                return NotFound();
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
            await _cityrepository.SaveChangesAsync();
            

            return NoContent();
        }

        ///
    }
}
