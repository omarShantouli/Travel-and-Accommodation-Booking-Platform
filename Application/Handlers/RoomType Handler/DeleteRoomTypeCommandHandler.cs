using Application.Commands.RoomType_Commands;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers.RoomType_Handler
{
    /// <summary>
    /// Handles the command to delete a room type.
    /// </summary>
    public class DeleteRoomTypeCommandHandler : IRequestHandler<DeleteRoomTypeCommand>
    {
        private readonly IRepository<RoomTypes> _roomTypeRepository;
        private readonly ILogger<DeleteRoomTypeCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteRoomTypeCommandHandler"/> class.
        /// </summary>
        /// <param name="roomTypeRepository">The repository for room type entities.</param>
        /// <param name="logger">The logger for capturing and logging information related to DeleteRoomTypeCommandHandler.</param>
        public DeleteRoomTypeCommandHandler(IRepository<RoomTypes> roomTypeRepository, ILogger<DeleteRoomTypeCommandHandler> logger)
        {
            _roomTypeRepository = roomTypeRepository;
            _logger = logger;
        }

        /// <summary>
        /// Handles the command to delete a room type.
        /// </summary>
        /// <param name="request">The command request containing room type ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public async Task Handle(DeleteRoomTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Handling DeleteRoomTypeCommand for RoomTypeId: {request.RoomTypeId}");

                var existingRoomType = await _roomTypeRepository.GetByIdAsync(request.RoomTypeId);

                if (existingRoomType == null)
                {
                    _logger.LogWarning($"Room type with ID {request.RoomTypeId} not found.");
                    throw new EntityNotFoundException($"Room type with ID {request.RoomTypeId} not found.");
                }

                await _roomTypeRepository.DeleteAsync(existingRoomType.Id);

                await _roomTypeRepository.SaveChangesAsync();

                _logger.LogInformation($"DeleteRoomTypeCommand handled successfully for RoomTypeId: {request.RoomTypeId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while handling DeleteRoomTypeCommand for RoomTypeId: {request.RoomTypeId}");
                throw;
            }
        }
    }
}
