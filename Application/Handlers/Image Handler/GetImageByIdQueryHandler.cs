using Application.DTOs;
using Application.Queries.Image_Queries;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers.Image_Handler
{
    /// <summary>
    /// Handles the query to get an image by its ID.
    /// </summary>
    public class GetImageByIdQueryHandler : IRequestHandler<GetImageByIdQuery, ImageDto>
    {
        private readonly IRepository<Images> _imageRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetImageByIdQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetImageByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="imageRepository">The repository for image entities.</param>
        /// <param name="mapper">The AutoMapper for mapping entities to DTOs.</param>
        /// <param name="logger">The logger for capturing and logging information related to GetImageByIdQueryHandler.</param>
        public GetImageByIdQueryHandler(IRepository<Images> imageRepository, IMapper mapper, ILogger<GetImageByIdQueryHandler> logger)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Handles the query to get an image by its ID.
        /// </summary>
        /// <param name="request">The query request containing the ImageId.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The ImageDto representing the image with the specified ID.</returns>
        public async Task<ImageDto> Handle(GetImageByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Handling GetImageByIdQuery for ImageId: {request.ImageId}");

                var image = await _imageRepository.GetByIdAsync(request.ImageId);

                if (image == null)
                {
                    _logger.LogWarning($"Image with ID {request.ImageId} not found.");
                    throw new EntityNotFoundException($"Image with ID {request.ImageId} not found.");
                }

                var imageDto = _mapper.Map<ImageDto>(image);

                _logger.LogInformation($"GetImageByIdQuery handled successfully for ImageId: {request.ImageId}");

                return imageDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while handling GetImageByIdQuery for ImageId: {request.ImageId}");
                throw;
            }
        }
    }
}
