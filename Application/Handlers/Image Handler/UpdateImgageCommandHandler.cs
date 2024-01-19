using Application.Commands.Image_Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers.Image_Handler
{
    /// <summary>
    /// Handles the command to update an image entity.
    /// </summary>
    public class UpdateImageCommandHandler : IRequestHandler<UpdateImageCommand>
    {
        private readonly IRepository<Images> _imageRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateImageCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateImageCommandHandler"/> class.
        /// </summary>
        /// <param name="imageRepository">The repository for image entities.</param>
        /// <param name="mapper">The AutoMapper for mapping entities to DTOs.</param>
        /// <param name="logger">The logger for logging messages.</param>
        public UpdateImageCommandHandler(IRepository<Images> imageRepository, IMapper mapper, ILogger<UpdateImageCommandHandler> logger)
        {
            _imageRepository = imageRepository ?? throw new ArgumentNullException(nameof(imageRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the command to update an image entity.
        /// </summary>
        /// <param name="request">The command request containing the ImageId and UpdatedImage.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async Task Handle(UpdateImageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Handling UpdateImageCommand for ImageId: {request.ImageId}");

                var existingImage = await _imageRepository.GetByIdAsync(request.ImageId);

                if (existingImage == null)
                {
                    _logger.LogWarning($"Image with ID {request.ImageId} not found.");
                    throw new EntityNotFoundException($"Image with ID {request.ImageId} not found.");
                }

                _mapper.Map(request.UpdatedImage, existingImage);

                await _imageRepository.SaveChangesAsync();

                _logger.LogInformation($"UpdateImageCommand handled successfully for ImageId: {request.ImageId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while handling UpdateImageCommand for ImageId: {request.ImageId}");
                throw;
            }
        }
    }
}
