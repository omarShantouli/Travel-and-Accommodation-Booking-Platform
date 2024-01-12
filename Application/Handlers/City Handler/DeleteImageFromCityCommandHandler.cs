using Application.Commands;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers
{
    /// <summary>
    /// Handles the command to delete an image from a city.
    /// </summary>
    public class DeleteImageFromCityCommandHandler : IRequestHandler<DeleteImageFromCityCommand>
    {
        private readonly IRepository<Images> _imageRepository;
        private readonly ILogger<DeleteImageFromCityCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteImageFromCityCommandHandler"/> class.
        /// </summary>
        /// <param name="imageRepository">The repository for image entities.</param>
        /// <param name="logger">The logger for capturing and logging information related to DeleteImageFromCityCommandHandler.</param>
        public DeleteImageFromCityCommandHandler(IRepository<Images> imageRepository, ILogger<DeleteImageFromCityCommandHandler> logger)
        {
            _imageRepository = imageRepository;
            _logger = logger;
        }

        /// <summary>
        /// Handles the command to delete an image from a city.
        /// </summary>
        /// <param name="request">The command request containing city and image IDs.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public async Task Handle(DeleteImageFromCityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Handling DeleteImageFromCityCommand for ImageId: {request.ImageId}, CityId: {request.CityId}");

                var imageToRemove = await _imageRepository.GetByIdAsync(request.ImageId);
                if (imageToRemove == null)
                {
                    _logger.LogWarning($"Image with ID {request.ImageId} not found.");
                    throw new EntityNotFoundException($"Image with ID {request.ImageId} is not found!");
                }

                if (imageToRemove.EntityId == request.CityId)
                {
                    await _imageRepository.DeleteAsync(request.ImageId);
                    await _imageRepository.SaveChangesAsync();
                    _logger.LogInformation($"DeleteImageFromCityCommand handled successfully for ImageId: {request.ImageId}, CityId: {request.CityId}");
                }
                else
                {
                    _logger.LogWarning($"City with ID {request.CityId} does not have an Image with ID {request.ImageId}.");
                    throw new EntityNotFoundException($"City with ID {request.CityId} does not have an Image with ID {request.ImageId}!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while handling DeleteImageFromCityCommand for ImageId: {request.ImageId}, CityId: {request.CityId}");
                throw;
            }
        }
    }
}
