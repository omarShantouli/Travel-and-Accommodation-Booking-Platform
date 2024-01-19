using Application.DTOs;
using Application.Queries.Rooms_Queries;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers.Rooms_Handler
{
    /// <summary>
    /// Handles the query to get a room by its ID.
    /// </summary>
    public class GetRoomByIdQueryHandler : IRequestHandler<GetRoomByIdQuery, RoomDto>  
    {
        private readonly IRepository<Rooms> _roomRepository; 
        private readonly IMapper _mapper;
        private readonly ILogger<GetRoomByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetRoomByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="roomRepository">The repository for room entities.</param>  
        /// <param name="mapper">The AutoMapper for mapping entities to DTOs.</param>
        /// <param name="logger">The logger for capturing and logging information related to GetRoomByIdQueryHandler.</param>
        public GetRoomByIdQueryHandler(IRepository<Rooms> roomRepository, IMapper mapper, ILogger<GetRoomByIdQueryHandler> logger)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Handles the query to get a room by its ID.
        /// </summary>
        /// <param name="request">The query request containing the RoomId.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The RoomDto representing the room with the specified ID.</returns>
        public async Task<RoomDto> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Handling GetRoomByIdQuery for RoomId: {request.RoomId}");  

                var room = await _roomRepository.GetByIdAsync(request.RoomId);  

                if (room == null)
                {
                    _logger.LogWarning($"Room with ID {request.RoomId} not found."); 
                    throw new EntityNotFoundException($"Room with ID {request.RoomId} not found.");  
                }

                var roomDto = _mapper.Map<RoomDto>(room);  

                _logger.LogInformation($"GetRoomByIdQuery handled successfully for RoomId: {request.RoomId}");

                return roomDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while handling GetRoomByIdQuery for RoomId: {request.RoomId}"); 
                throw;
            }
        }
    }
}
