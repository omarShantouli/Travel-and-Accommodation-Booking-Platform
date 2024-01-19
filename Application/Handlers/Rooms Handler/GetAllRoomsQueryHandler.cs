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
    /// Handles the query to get all rooms.
    /// </summary>
    public class GetAllRoomsQueryHandler : IRequestHandler<GetAllRoomsQuery, List<RoomDto>>  // Adjust based on your actual query
    {
        private readonly IRepository<Rooms> _roomRepository;  // Adjust based on your actual entity
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllRoomsQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllRoomsQueryHandler"/> class.
        /// </summary>
        /// <param name="roomRepository">The repository for room entities.</param>  // Adjust based on your actual entity
        /// <param name="mapper">The AutoMapper for mapping entities to DTOs.</param>
        /// <param name="logger">The logger for capturing and logging information related to GetAllRoomsQueryHandler.</param>
        public GetAllRoomsQueryHandler(IRepository<Rooms> roomRepository, IMapper mapper, ILogger<GetAllRoomsQueryHandler> logger)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Handles the query to get all rooms.
        /// </summary>
        /// <param name="request">The query request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of RoomDto representing all rooms.</returns>
        public async Task<List<RoomDto>> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Handling GetAllRoomsQuery.");  // Adjust based on your actual query

                var rooms = _roomRepository.GetAll();  // Adjust based on your actual entity

                if (rooms == null)
                {
                    _logger.LogWarning("No rooms found.");  // Adjust based on your actual entity
                    throw new EntityNotFoundException("There are no rooms found.");  // Adjust based on your actual entity
                }

                var roomsDto = _mapper.Map<List<RoomDto>>(rooms);  // Adjust based on your actual entity

                _logger.LogInformation("GetAllRoomsQuery handled successfully.");  // Adjust based on your actual query

                return roomsDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while handling GetAllRoomsQuery.");  // Adjust based on your actual query
                throw;
            }
        }
    }
}
