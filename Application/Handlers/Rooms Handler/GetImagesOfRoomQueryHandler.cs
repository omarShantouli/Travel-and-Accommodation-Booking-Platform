using Application.DTOs;
using Application.Queries.Rooms_Queries;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="GetImagesOfRoomQueryHandler"/> class.
        /// </summary>
        /// <param name="imageRepository">The repository for image entities.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        public GetImagesOfRoomQueryHandler(IRepository<Images> imageRepository, IMapper mapper)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;
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

                LogRoomImagesRetrieved(request.RoomId);

                return imagesDtos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while handling GetImagesOfRoomQuery: {ex.Message}");
                throw;
            }
        }

        private void LogRoomImagesRetrieved(Guid roomId)
        {
            Console.WriteLine($"Images retrieved for Room with ID {roomId}.");
        }
    }
}
