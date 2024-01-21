using Application.Commands.RoomType_Commands;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers.RoomType_Handler
{
    /// <summary>
    /// Handles the command to delete a room by RoomId and RoomTypeId.
    /// </summary>
    public class DeleteRoomByRoomTypeCommandHandler : IRequestHandler<DeleteRoomByRoomTypeCommand>
    {
        private readonly IRepository<Rooms> _roomRepository;
        private readonly ILogger<DeleteRoomByRoomTypeCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteRoomByRoomTypeCommandHandler"/> class.
        /// </summary>
        /// <param name="roomRepository">The repository for room entities.</param>
        /// <param name="logger">The logger for capturing and logging information related to DeleteRoomByRoomTypeCommandHandler.</param>
        public DeleteRoomByRoomTypeCommandHandler(IRepository<Rooms> roomRepository, ILogger<DeleteRoomByRoomTypeCommandHandler> logger)
        {
            _roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the command to delete a room by RoomId and RoomTypeId.
        /// </summary>
        /// <param name="request">The command request containing RoomId and RoomTypeId.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async Task Handle(DeleteRoomByRoomTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Deleting room with RoomId {request.RoomId} from RoomTypeId {request.RoomTypeId}");

                var room = await _roomRepository.GetByIdAsync(request.RoomId);

                if (room == null)
                {
                    _logger.LogWarning($"Room with ID {request.RoomId} not found.");
                    throw new EntityNotFoundException($"Room with ID {request.RoomId} not found.");
                }

                if (room.RoomTypeId != request.RoomTypeId)
                {
                    _logger.LogWarning($"Room with ID {request.RoomId} does not belong to RoomTypeId {request.RoomTypeId}");
                    throw new InvalidOperationException($"Room with ID {request.RoomId} does not belong to RoomTypeId {request.RoomTypeId}");
                }

                await _roomRepository.DeleteAsync(room.Id);

                _logger.LogInformation($"Successfully deleted room with RoomId {request.RoomId} from RoomTypeId {request.RoomTypeId}");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while handling DeleteRoomByRoomTypeCommand for RoomId: {request.RoomId}, RoomTypeId: {request.RoomTypeId}");
                throw;
            }
        }
    }
}
