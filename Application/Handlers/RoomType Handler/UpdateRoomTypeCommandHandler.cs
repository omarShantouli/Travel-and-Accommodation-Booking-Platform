using Application.Commands.RoomType_Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers.RoomType_Handler
{
    /// <summary>
    /// Handles the command to update a room type entity.
    /// </summary>
    public class UpdateRoomTypeCommandHandler : IRequestHandler<UpdateRoomTypeCommand>
    {
        private readonly IRepository<RoomTypes> _roomTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateRoomTypeCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateRoomTypeCommandHandler"/> class.
        /// </summary>
        /// <param name="roomTypeRepository">The repository for room type entities.</param>
        /// <param name="mapper">The AutoMapper for mapping entities to DTOs.</param>
        /// <param name="logger">The logger for logging messages.</param>
        public UpdateRoomTypeCommandHandler(IRepository<RoomTypes> roomTypeRepository, IMapper mapper, ILogger<UpdateRoomTypeCommandHandler> logger)
        {
            _roomTypeRepository = roomTypeRepository ?? throw new ArgumentNullException(nameof(roomTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the command to update a room type entity.
        /// </summary>
        /// <param name="request">The command request containing the RoomTypeId and UpdatedRoomType.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async Task Handle(UpdateRoomTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Handling UpdateRoomTypeCommand for RoomTypeId: {request.RoomTypeId}");

                var existingRoomType = await _roomTypeRepository.GetByIdAsync(request.RoomTypeId);

                if (existingRoomType == null)
                {
                    _logger.LogWarning($"Room type with ID {request.RoomTypeId} not found.");
                    throw new EntityNotFoundException($"Room type with ID {request.RoomTypeId} not found.");
                }

                _mapper.Map(request.UpdatedRoomType, existingRoomType);

                await _roomTypeRepository.SaveChangesAsync();

                _logger.LogInformation($"UpdateRoomTypeCommand handled successfully for RoomTypeId: {request.RoomTypeId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while handling UpdateRoomTypeCommand for RoomTypeId: {request.RoomTypeId}");
                throw;
            }
        }
    }
}
