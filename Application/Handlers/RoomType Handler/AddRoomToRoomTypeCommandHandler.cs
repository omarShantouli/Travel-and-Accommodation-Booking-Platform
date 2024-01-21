using Application.Commands.RoomType_Commands;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers.RoomType_Handler
{
    /// <summary>
    /// Handles the command to add a room to a specific room type.
    /// </summary>
    public class AddRoomToRoomTypeCommandHandler : IRequestHandler<AddRoomToRoomTypeCommand>
    {
        private readonly IRepository<Rooms> _roomRepository;
        private readonly ILogger<AddRoomToRoomTypeCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddRoomToRoomTypeCommandHandler"/> class.
        /// </summary>
        /// <param name="roomRepository">The repository for room entities.</param>
        /// <param name="logger">The logger for capturing and logging information related to AddRoomToRoomTypeCommandHandler.</param>
        public AddRoomToRoomTypeCommandHandler(IRepository<Rooms> roomRepository, ILogger<AddRoomToRoomTypeCommandHandler> logger)
        {
            _roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the command to add a room to a specific room type.
        /// </summary>
        /// <param name="request">The command request containing RoomTypeId and RoomId.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if the RoomTypeId was successfully updated; false if it was already the same.</returns>
        public async Task Handle(AddRoomToRoomTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Adding room with RoomId {request.RoomId} to RoomTypeId {request.RoomTypeId}");

                var room = await _roomRepository.GetByIdAsync(request.RoomId);

                if (room == null)
                {
                    _logger.LogWarning($"Room with ID {request.RoomId} not found.");
                    throw new EntityNotFoundException($"Room with ID {request.RoomId} not found.");
                }

                room.RoomTypeId = request.RoomTypeId;
                room.Id = Guid.NewGuid();

                await _roomRepository.CreateAsync(room);

                _logger.LogInformation($"Successfully added room with RoomId {request.RoomId} to RoomTypeId {request.RoomTypeId}");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while handling AddRoomToRoomTypeCommand for RoomId: {request.RoomId}, RoomTypeId: {request.RoomTypeId}");
                throw;
            }
        }
    }
}
