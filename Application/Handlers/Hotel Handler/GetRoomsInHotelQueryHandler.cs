using Application.DTOs;
using Application.Queries;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers
{
    /// <summary>
    /// Handles the query to get rooms in a hotel.
    /// </summary>
    public class GetRoomsInHotelQueryHandler : IRequestHandler<GetRoomsInHotelQuery, List<RoomDto>>
    {
        private readonly IRepository<Hotels> _hotelRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetRoomsInHotelQueryHandler"/> class.
        /// </summary>
        /// <param name="hotelRepository">The repository for hotel entities.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        public GetRoomsInHotelQueryHandler(IRepository<Hotels> hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the query to get rooms in a hotel.
        /// </summary>
        /// <param name="request">The query request containing hotel information.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of room DTOs.</returns>
        public async Task<List<RoomDto>> Handle(GetRoomsInHotelQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var hotel = await _hotelRepository.GetByIdAsync(request.HotelId);
                if (hotel == null)
                {
                    LogHotelNotFound(request.HotelId);
                    throw new EntityNotFoundException($"Hotel with ID {request.HotelId} not found.");
                }

                var rooms = hotel.Rooms;
                var roomsDto = _mapper.Map<List<RoomDto>>(rooms);

                LogHotelRoomsInformation(request.HotelId, roomsDto.Count);

                return roomsDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while handling GetRoomsInHotelQuery: {ex.Message}");
                throw;
            }
        }

        private void LogHotelNotFound(Guid hotelId)
        {
            Console.WriteLine($"Hotel with ID {hotelId} not found.");
        }

        private void LogHotelRoomsInformation(Guid hotelId, int numberOfRooms)
        {
            Console.WriteLine($"Rooms in HotelId: {hotelId} - Count: {numberOfRooms}");
        }
    }
}
