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
    /// Handles the query to get all images.
    /// </summary>
    public class GetAllImagesQueryHandler : IRequestHandler<GetAllImagesQuery, List<ImageDto>>
    {
        private readonly IRepository<Images> _imageRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllImagesQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllImagesQueryHandler"/> class.
        /// </summary>
        /// <param name="imageRepository">The repository for image entities.</param>
        /// <param name="mapper">The AutoMapper for mapping entities to DTOs.</param>
        /// <param name="logger">The logger for capturing and logging information related to GetAllImagesQueryHandler.</param>
        public GetAllImagesQueryHandler(IRepository<Images> imageRepository, IMapper mapper, ILogger<GetAllImagesQueryHandler> logger)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Handles the query to get all images.
        /// </summary>
        /// <param name="request">The query request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of ImageDto representing all images.</returns>
        public async Task<List<ImageDto>> Handle(GetAllImagesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Handling GetAllImagesQuery.");

                var images = _imageRepository.GetAll();

                if (images == null)
                {
                    _logger.LogWarning("No images found.");
                    throw new EntityNotFoundException("There are no images found.");
                }

                var imagesDto = _mapper.Map<List<ImageDto>>(images);

                _logger.LogInformation("GetAllImagesQuery handled successfully.");

                return imagesDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while handling GetAllImagesQuery.");
                throw;
            }
        }
    }
}
