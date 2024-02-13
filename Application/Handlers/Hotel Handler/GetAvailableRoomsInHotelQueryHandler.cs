using Application.DTOs;
using Application.Queries;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Reflection.Emit;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers
{
    /// <summary>
    /// Handles the query to get available rooms in a hotel.
    /// </summary>
    public class GetAvailableRoomsInHotelQueryHandler : IRequestHandler<GetAvailableRoomsInHotelQuery, List<RoomDto>>
    {
        private readonly IRepository<Rooms> _roomRepository;
        private readonly IRepository<Bookings> _bookingRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAvailableRoomsInHotelQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAvailableRoomsInHotelQueryHandler"/> class.
        /// </summary>
        /// <param name="roomRepository">The repository for room entities.</param>
        /// <param name="bookingRepository">The repository for booking entities.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        public GetAvailableRoomsInHotelQueryHandler(IRepository<Rooms> roomRepository,
                                                    IRepository<Bookings> bookingRepository, IMapper mapper,
                                                    ILogger<GetAvailableRoomsInHotelQueryHandler> logger)
        {
            _roomRepository = roomRepository;
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Handles the query to get available rooms in a hotel.
        /// </summary>
        /// <param name="request">The query request containing hotel information.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of available room DTOs.</returns>
        public async Task<List<RoomDto>> Handle(GetAvailableRoomsInHotelQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var bookedRoomIds = _bookingRepository.GetAll().Where(b => b.CheckOutDate >= DateTime.Now)
                                                           .Select(b => b.RoomId).ToList();

                var availableRooms = _roomRepository.GetAll().Where(r => r.HotelId == request.HotelId
                                                                    && !bookedRoomIds.Contains(r.Id));

                var availableRoomsDto = _mapper.Map<List<RoomDto>>(availableRooms);

                _logger.LogInformation($"Available rooms in HotelId: {request.HotelId} - Count: {availableRoomsDto.Count}");

                return availableRoomsDto;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"An error occurred while handling GetAvailableRoomsInHotelQuery: {ex.Message}");
                throw;
            }
        }
    }
}
