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
    /// Handles the query to get all room types.
    /// </summary>
    public class GetAllRoomTypesQueryHandler : IRequestHandler<GetAllRoomTypesQuery, List<RoomTypeDto>>
    {
        private readonly IRepository<RoomTypes> _roomTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllRoomTypesQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllRoomTypesQueryHandler"/> class.
        /// </summary>
        /// <param name="roomTypeRepository">The repository for room type entities.</param>
        /// <param name="mapper">The AutoMapper for mapping entities to DTOs.</param>
        /// <param name="logger">The logger for capturing and logging information related to GetAllRoomTypesQueryHandler.</param>
        public GetAllRoomTypesQueryHandler(IRepository<RoomTypes> roomTypeRepository, IMapper mapper, ILogger<GetAllRoomTypesQueryHandler> logger)
        {
            _roomTypeRepository = roomTypeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Handles the query to get all room types.
        /// </summary>
        /// <param name="request">The query request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of RoomTypeDto representing all room types.</returns>
        public async Task<List<RoomTypeDto>> Handle(GetAllRoomTypesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Handling GetAllRoomTypesQuery.");

                var roomTypes = _roomTypeRepository.GetAll();

                if (roomTypes == null)
                {
                    _logger.LogWarning("No room types found.");
                    throw new EntityNotFoundException("There are no room types found.");
                }

                var roomTypesDto = _mapper.Map<List<RoomTypeDto>>(roomTypes);

                _logger.LogInformation("GetAllRoomTypesQuery handled successfully.");

                return roomTypesDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while handling GetAllRoomTypesQuery.");
                throw;
            }
        }
    }
}
