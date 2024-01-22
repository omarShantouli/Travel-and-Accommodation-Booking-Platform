using Application.DTOs;
using Application.Queries.Home;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers.Home_Handlers
{
    /// <summary>
    /// Handles the search query for hotels based on specified parameters.
    /// </summary>
    public class SearchHotelQueryHandler : IRequestHandler<SearchHotelQuery, List<HotelDto>>
    {
        private readonly IRepository<Hotels> _hotelRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<SearchHotelQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchHotelQueryHandler"/> class.
        /// </summary>
        /// <param name="hotelRepository">The repository for hotel entities.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing and logging information related to SearchHotelQueryHandler.</param>
        public SearchHotelQueryHandler(IRepository<Hotels> hotelRepository, IMapper mapper, ILogger<SearchHotelQueryHandler> logger)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Handles the search query to find hotels based on the specified parameters.
        /// </summary>
        /// <param name="request">The search query request containing hotel search parameters.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A list of hotel DTOs matching the search criteria.</returns>
        public async Task<List<HotelDto>> Handle(SearchHotelQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var filteredHotels = await SearchHotelsAsync(request);

                var hotelDtos = _mapper.Map<List<HotelDto>>(filteredHotels);

                return hotelDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while handling SearchHotelQuery.");
                throw;
            }
        }

        /// <summary>
        /// Searches for hotels based on the specified search query parameters.
        /// </summary>
        /// <param name="request">The search query parameters.</param>
        /// <returns>A list of hotels matching the search criteria.</returns>
        private async Task<List<Hotels>> SearchHotelsAsync(SearchHotelQuery request)
        {
            var allHotels = _hotelRepository.GetAll();
            
            foreach(var hotel in allHotels)
            {
                Console.WriteLine(hotel.Name + " => " + hotel.City.Name);
                foreach(var room in hotel.Rooms)
                    Console.WriteLine(room.Bookings.Count());
            }
            var checkInDate = DateTime.Parse(request.CheckInDate);
            var checkOutDate = DateTime.Parse(request.CheckOutDate);
            var filteredHotels = allHotels
                .Where(hotel => hotel != null &&
                                hotel.City != null &&
                                hotel.City.Name.ToLower() == request.City.ToLower() &&
                                hotel.Rooms != null &&
                                hotel.Rooms.Count() >= request.NumberOfRooms &&
                                hotel.Rating >= request.StarRate &&
                                hotel.Rooms.Any(room => room.AdultsCapacity >= request.AdultsCapacity &&
                                                       room.ChildrenCapacity >= request.ChildrenCapacity &&
                                                       ( room.Bookings.Count() == 0 ||
                                                         room.Bookings.Any(booking =>
                                                         //  true || 
                                                           checkInDate > booking.CheckOutDate ||
                                                           checkOutDate < booking.CheckInDate )
                                                       )))
                .ToList();

            return filteredHotels;
        }
    }
}
