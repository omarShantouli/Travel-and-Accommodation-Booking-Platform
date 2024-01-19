using Application.Commands.Rooms_Commands;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers.Rooms_Handler
{
    /// <summary>
    /// Handles the command to delete a room.
    /// </summary>
    public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand>  
    {
        private readonly IRepository<Rooms> _roomRepository; 
        private readonly ILogger<DeleteRoomCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteRoomCommandHandler"/> class.
        /// </summary>
        /// <param name="roomRepository">The repository for room entities.</param> 
        /// <param name="logger">The logger for capturing and logging information related to DeleteRoomCommandHandler.</param>
        public DeleteRoomCommandHandler(IRepository<Rooms> roomRepository, ILogger<DeleteRoomCommandHandler> logger)
        {
            _roomRepository = roomRepository;
            _logger = logger;
        }

        /// <summary>
        /// Handles the command to delete a room.
        /// </summary>
        /// <param name="request">The command request containing room ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public async Task Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Handling DeleteRoomCommand for RoomId: {request.RoomId}"); 

                var existingRoom = await _roomRepository.GetByIdAsync(request.RoomId);

                if (existingRoom == null)
                {
                    _logger.LogWarning($"Room with ID {request.RoomId} not found."); 
                    throw new EntityNotFoundException($"Room with ID {request.RoomId} not found."); 
                }

                await _roomRepository.DeleteAsync(existingRoom.Id);

                await _roomRepository.SaveChangesAsync();

                _logger.LogInformation($"DeleteRoomCommand handled successfully for RoomId: {request.RoomId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while handling DeleteRoomCommand for RoomId: {request.RoomId}");
                throw;
            }
        }
    }
}
