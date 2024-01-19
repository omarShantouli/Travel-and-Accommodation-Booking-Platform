using Application.Commands.Image_Commands;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers.Image_Handler
{
    /// <summary>
    /// Handles the command to delete an image.
    /// </summary>
    public class DeleteImageCommandHandler : IRequestHandler<DeleteImageCommand>
    {
        private readonly IRepository<Images> _imageRepository;
        private readonly ILogger<DeleteImageCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteImageCommandHandler"/> class.
        /// </summary>
        /// <param name="imageRepository">The repository for image entities.</param>
        /// <param name="logger">The logger for capturing and logging information related to DeleteImageCommandHandler.</param>
        public DeleteImageCommandHandler(IRepository<Images> imageRepository, ILogger<DeleteImageCommandHandler> logger)
        {
            _imageRepository = imageRepository;
            _logger = logger;
        }

        /// <summary>
        /// Handles the command to delete an image.
        /// </summary>
        /// <param name="request">The command request containing image ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public async Task Handle(DeleteImageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Handling DeleteImageCommand for ImageId: {request.ImageId}");

                var existingImage = await _imageRepository.GetByIdAsync(request.ImageId);

                if (existingImage == null)
                {
                    _logger.LogWarning($"Image with ID {request.ImageId} not found.");
                    throw new EntityNotFoundException($"Image with ID {request.ImageId} not found.");
                }

                await _imageRepository.DeleteAsync(existingImage.Id);

                await _imageRepository.SaveChangesAsync();

                _logger.LogInformation($"DeleteImageCommand handled successfully for ImageId: {request.ImageId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while handling DeleteImageCommand for ImageId: {request.ImageId}");
                throw;
            }
        }
    }
}
