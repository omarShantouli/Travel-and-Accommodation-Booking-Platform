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
    /// Handles the command to create a new image.
    /// </summary>
    public class CreateImageCommandHandler : IRequestHandler<CreateImageCommand>
    {
        private readonly IRepository<Images> _imageRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateImageCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateImageCommandHandler"/> class.
        /// </summary>
        /// <param name="imageRepository">The repository for image entities.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing and logging information related to CreateImageCommandHandler.</param>
        public CreateImageCommandHandler(IRepository<Images> imageRepository, IMapper mapper, ILogger<CreateImageCommandHandler> logger)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Handles the command to create a new image.
        /// </summary>
        /// <param name="request">The command request containing image information.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public async Task Handle(CreateImageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Image == null)
                {
                    _logger.LogError("Image object is null.");
                    throw new EntityNotFoundException("Image object is null.");
                }

                var imageToAdd = _mapper.Map<Images>(request.Image);
                await _imageRepository.CreateAsync(imageToAdd);
                await _imageRepository.SaveChangesAsync();

                _logger.LogInformation($"Image created successfully. ImageId: {imageToAdd.Id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while handling CreateImageCommand.");
                throw;
            }
        }
    }
}
