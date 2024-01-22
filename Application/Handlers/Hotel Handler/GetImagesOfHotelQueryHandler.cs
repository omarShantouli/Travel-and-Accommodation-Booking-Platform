using Application.DTOs;
using Application.Queries;
using AutoMapper;
using Castle.Core.Logging;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers.Hotel_Handler
{
    /// <summary>
    /// Handles the query to get images of a hotel.
    /// </summary>
    public class GetImagesOfHotelQueryHandler : IRequestHandler<GetImagesOfHotelQuery, List<ImageDto>>
    {
        private readonly IRepository<Images> _imageRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetImagesOfHotelQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetImagesOfHotelQueryHandler"/> class.
        /// </summary>
        /// <param name="imageRepository">The repository for image entities.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        public GetImagesOfHotelQueryHandler(IRepository<Images> imageRepository, IMapper mapper,
                                            ILogger<GetImagesOfHotelQueryHandler> logger)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Handles the query to get images of a hotel.
        /// </summary>
        /// <param name="request">The query request containing hotel information.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of image DTOs.</returns>
        public async Task<List<ImageDto>> Handle(GetImagesOfHotelQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var images = _imageRepository.GetAll();

                var hotelImages = images.Where(image => image.EntityId == request.HotelId
                                              && image.EntityType == EntityType.Hotel.ToString());

                var imagesDtos = _mapper.Map<List<ImageDto>>(hotelImages);

                _logger.LogInformation($"Images of HotelId: {request.HotelId} - Count: {imagesDtos.Count}");

                return imagesDtos;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"An error occurred while handling GetImagesOfHotelQuery: {ex.Message}");
                throw;
            }
        }
    }
}
