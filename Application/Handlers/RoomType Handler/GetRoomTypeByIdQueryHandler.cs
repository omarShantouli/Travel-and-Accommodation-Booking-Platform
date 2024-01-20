using Application.DTOs;
using Application.Queries.RoomType_Queries;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers.RoomType_Handler
{
    /// <summary>
    /// Handles the query to get a room type by its ID.
    /// </summary>
    public class GetRoomTypeByIdQueryHandler : IRequestHandler<GetRoomTypeByIdQuery, RoomTypeDto>
    {
        private readonly IRepository<RoomTypes> _roomTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetRoomTypeByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetRoomTypeByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="roomTypeRepository">The repository for room type entities.</param>
        /// <param name="mapper">The AutoMapper for mapping entities to DTOs.</param>
        /// <param name="logger">The logger for capturing and logging information related to GetRoomTypeByIdQueryHandler.</param>
        public GetRoomTypeByIdQueryHandler(IRepository<RoomTypes> roomTypeRepository, IMapper mapper, ILogger<GetRoomTypeByIdQueryHandler> logger)
        {
            _roomTypeRepository = roomTypeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Handles the query to get a room type by its ID.
        /// </summary>
        /// <param name="request">The query request containing the RoomTypeId.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The RoomTypeDto representing the room type with the specified ID.</returns>
        public async Task<RoomTypeDto> Handle(GetRoomTypeByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Handling GetRoomTypeByIdQuery for RoomTypeId: {request.RoomTypeId}");

                var roomType = await _roomTypeRepository.GetByIdAsync(request.RoomTypeId);

                if (roomType == null)
                {
                    _logger.LogWarning($"Room type with ID {request.RoomTypeId} not found.");
                    throw new EntityNotFoundException($"Room type with ID {request.RoomTypeId} not found.");
                }

                var roomTypeDto = _mapper.Map<RoomTypeDto>(roomType);

                _logger.LogInformation($"GetRoomTypeByIdQuery handled successfully for RoomTypeId: {request.RoomTypeId}");

                return roomTypeDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while handling GetRoomTypeByIdQuery for RoomTypeId: {request.RoomTypeId}");
                throw;
            }
        }
    }
}
