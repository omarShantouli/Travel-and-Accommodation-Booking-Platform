using Application.DTOs;
using Application.Queries;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers
{
    /// <summary>
    /// Handles the query to retrieve a list of images associated with a city.
    /// </summary>
    public class GetImagesOfCityQueryHandler : IRequestHandler<GetImagesOfCityQuery, List<ImageDto>>
    {
        private readonly IRepository<Images> _imageRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetImagesOfCityQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetImagesOfCityQueryHandler"/> class.
        /// </summary>
        /// <param name="imageRepository">The repository for image entities.</param>
        /// <param name="mapper">The AutoMapper for mapping entities to DTOs.</param>
        /// <param name="logger">The logger for logging messages.</param>
        public GetImagesOfCityQueryHandler(IRepository<Images> imageRepository, IMapper mapper, ILogger<GetImagesOfCityQueryHandler> logger)
        {
            _imageRepository = imageRepository ?? throw new ArgumentNullException(nameof(imageRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the query to retrieve a list of images associated with a city.
        /// </summary>
        /// <param name="request">The query request containing the CityId.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A list of ImageDto representing the images associated with the specified city.</returns>
        public async Task<List<ImageDto>> Handle(GetImagesOfCityQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Handling GetImagesOfCityQuery for CityId: {request.CityId}");

                var images = _imageRepository.GetAll();

                if (images == null)
                {
                    _logger.LogWarning("No images found.");
                    throw new EntityNotFoundException("There are no images found.");
                }

                var cityImages = images.Where(image => image.EntityId == request.CityId
                                                      && image.EntityType == EntityType.City.ToString());

                if (cityImages == null)
                {
                    _logger.LogWarning($"No images found for city with id {request.CityId}");
                    throw new EntityNotFoundException($"There are no images found for city with id {request.CityId}");
                }

                var imagesDtos = _mapper.Map<List<ImageDto>>(cityImages);

                _logger.LogInformation($"GetImagesOfCityQuery handled successfully for CityId: {request.CityId}");

                return imagesDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while handling GetImagesOfCityQuery for CityId: {request.CityId}");
                throw new ApplicationException("Error occurred while handling GetImagesOfCityQuery.", ex);
            }
        }
    }
}
