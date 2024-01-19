using Application.Commands.Rooms_Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers.Rooms_Handler
{
    /// <summary>
    /// Handles the command to add an image to a room.
    /// </summary>
    public class AddImageToRoomCommandHandler : IRequestHandler<AddImageToRoomCommand>
    {
        private readonly IRepository<Images> _imageRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddImageToRoomCommandHandler"/> class.
        /// </summary>
        /// <param name="imageRepository">The repository for image entities.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        public AddImageToRoomCommandHandler(IRepository<Images> imageRepository, IMapper mapper)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the command to add an image to a room.
        /// </summary>
        /// <param name="request">The command request containing image and room information.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public async Task Handle(AddImageToRoomCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var image = _mapper.Map<Images>(request.Image);
                image.EntityType = EntityType.Room.ToString();
                image.EntityId = request.RoomId;

                await _imageRepository.CreateAsync(image);
                await _imageRepository.SaveChangesAsync();

                LogImageAddedToRoom(request.RoomId, image.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while handling AddImageToRoomCommand: {ex.Message}");
                throw;
            }
        }

        private void LogImageAddedToRoom(Guid roomId, Guid imageId)
        {
            Console.WriteLine($"Image with ID {imageId} added to Room with ID {roomId}.");
        }
    }
}
