using Application.DTOs;
using Application.Queries.Rooms_Queries;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers.GetImagesOfRoomQueryHandler
{
    /// <summary>
    /// Handles the query to retrieve images of a room.
    /// </summary>
    public class GetImagesOfRoomQueryHandler : IRequestHandler<GetImagesOfRoomQuery, List<ImageDto>>
    {
        private readonly IRepository<Images> _imageRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetImagesOfRoomQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetImagesOfRoomQueryHandler"/> class.
        /// </summary>
        /// <param name="imageRepository">The repository for image entities.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        public GetImagesOfRoomQueryHandler(IRepository<Images> imageRepository, IMapper mapper,
                                            ILogger<GetImagesOfRoomQueryHandler> logger)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Handles the query to retrieve images of a room.
        /// </summary>
        /// <param name="request">The query request containing room information.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public async Task<List<ImageDto>> Handle(GetImagesOfRoomQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var images = _imageRepository.GetAll();

                var roomImages = images.Where(image => image.EntityId == request.RoomId
                                              && image.EntityType == EntityType.Room.ToString());

                var imagesDtos = _mapper.Map<List<ImageDto>>(roomImages);

                _logger.LogInformation($"Images retrieved for Room with ID {request.RoomId}.");


                return imagesDtos;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"An error occurred while handling GetImagesOfRoomQuery: {ex.Message}");
                throw;
            }
        }
    }
}
