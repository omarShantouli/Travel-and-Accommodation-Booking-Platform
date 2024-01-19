using Application.Commands.Image_Commands;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers.Image_Handler
{
    /// <summary>
    /// Handles the command to apply partial updates to an image entity.
    /// </summary>
    public class PatchImageCommandHandler : IRequestHandler<PatchImageCommand>
    {
        private readonly IRepository<Images> _imageRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PatchImageCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PatchImageCommandHandler"/> class.
        /// </summary>
        /// <param name="imageRepository">The repository for image entities.</param>
        /// <param name="mapper">The AutoMapper for mapping entities to DTOs.</param>
        /// <param name="logger">The logger for logging messages.</param>
        public PatchImageCommandHandler(IRepository<Images> imageRepository, IMapper mapper, ILogger<PatchImageCommandHandler> logger)
        {
            _imageRepository = imageRepository ?? throw new ArgumentNullException(nameof(imageRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the command to apply partial updates to an image entity.
        /// </summary>
        /// <param name="request">The command request containing the ImageId and patchDocument.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async Task Handle(PatchImageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Handling PatchImageCommand for ImageId: {request.ImageId}");

                var image = await _imageRepository.GetByIdAsync(request.ImageId);

                if (image == null)
                {
                    _logger.LogWarning($"Image with ID {request.ImageId} not found.");
                    throw new EntityNotFoundException($"Image with ID {request.ImageId} not found.");
                }

                var imageDto = _mapper.Map<ImageDto>(image);

                request.patchDocument.ApplyTo(imageDto);

                _mapper.Map(imageDto, image);

                await _imageRepository.SaveChangesAsync();

                _logger.LogInformation($"PatchImageCommand handled successfully for ImageId: {request.ImageId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while handling PatchImageCommand for ImageId: {request.ImageId}");
                throw;
            }
        }
    }
}
