using Application.DTOs;
using Application.Queries.RoomType_Queries;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers.RoomType_Handler
{
    /// <summary>
    /// Handles the query to retrieve a list of rooms of a specific room type.
    /// </summary>
    public class GetRoomsOfRoomTypeQueryHandler : IRequestHandler<GetRoomsOfRoomTypeQuery, List<RoomDto>>
    {
        private readonly IRepository<Rooms> _roomRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetRoomsOfRoomTypeQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetRoomsOfRoomTypeQueryHandler"/> class.
        /// </summary>
        /// <param name="hotelRepository">The repository for hotel entities.</param>
        /// <param name="mapper">The AutoMapper for mapping entities to DTOs.</param>
        /// <param name="logger">The logger for capturing and logging information related to GetRoomsOfRoomTypeQueryHandler.</param>
        public GetRoomsOfRoomTypeQueryHandler(IRepository<Rooms> roomRepository, IMapper mapper, ILogger<GetRoomsOfRoomTypeQueryHandler> logger)
        {
            _roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger;
        }

        /// <summary>
        /// Handles the query to retrieve a list of rooms of a specific room type.
        /// </summary>
        /// <param name="request">The query request containing the RoomTypeId.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A list of RoomDto representing the rooms of the specified room type in the specified hotel.</returns>
        public async Task<List<RoomDto>> Handle(GetRoomsOfRoomTypeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var rooms = _roomRepository.GetAll().Where(r => r.RoomTypeId == request.RoomTypeId);
                var roomDtos = _mapper.Map<List<RoomDto>>(rooms);

                return roomDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while handling GetRoomsOfRoomTypeQuery for RoomTypeId: {request.RoomTypeId}");
                throw;
            }
        }
    }
}
