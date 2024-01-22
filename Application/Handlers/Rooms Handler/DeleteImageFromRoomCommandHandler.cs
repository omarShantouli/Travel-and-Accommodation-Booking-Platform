using Application.Commands.Rooms_Commands;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers.Rooms_Handler
{
    /// <summary>
    /// Handles the command to delete an image from a room.
    /// </summary>
    public class DeleteImageFromRoomCommandHandler : IRequestHandler<DeleteImageFromRoomCommand>
    {
        private readonly IRepository<Images> _imageRepository;
        private readonly ILogger<DeleteImageFromRoomCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteImageFromRoomCommandHandler"/> class.
        /// </summary>
        /// <param name="imageRepository">The repository for image entities.</param>
        public DeleteImageFromRoomCommandHandler(IRepository<Images> imageRepository, 
                                                    ILogger<DeleteImageFromRoomCommandHandler> logger)
        {
            _imageRepository = imageRepository;
        }

        /// <summary>
        /// Handles the command to delete an image from a room.
        /// </summary>
        /// <param name="request">The command request containing image and room information.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public async Task Handle(DeleteImageFromRoomCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var imageToRemove = await _imageRepository.GetByIdAsync(request.ImageId);
                if (imageToRemove == null)
                {
                    throw new EntityNotFoundException($"Image with ID {request.ImageId} is not found!");
                }

                if (imageToRemove.EntityId == request.RoomId)
                {
                    await _imageRepository.DeleteAsync(request.ImageId);
                    await _imageRepository.SaveChangesAsync();

                    _logger.LogInformation($"Image with ID {request.ImageId} deleted from Room with ID {request.RoomId}.");

                }
                else
                {
                    throw new EntityNotFoundException($"Room with ID {request.RoomId} does not have an Image with ID {request.ImageId}!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"An error occurred while handling DeleteImageFromRoomCommand: {ex.Message}");
                throw;
            }
        }
    }
}
