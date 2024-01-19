using Application.Commands.Hotel_Commands;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers.Hotel_Handler
{
    /// <summary>
    /// Handles the command to delete an image from a hotel.
    /// </summary>
    public class DeleteImageFromHotelCommandHandler : IRequestHandler<DeleteImageFromHotelCommand>
    {
        private readonly IRepository<Images> _imageRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteImageFromHotelCommandHandler"/> class.
        /// </summary>
        /// <param name="imageRepository">The repository for image entities.</param>
        public DeleteImageFromHotelCommandHandler(IRepository<Images> imageRepository)
        {
            _imageRepository = imageRepository;
        }

        /// <summary>
        /// Handles the command to delete an image from a hotel.
        /// </summary>
        /// <param name="request">The command request containing image and hotel information.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public async Task Handle(DeleteImageFromHotelCommand request, CancellationToken cancellationToken)
        {
            var imageToRemove = await _imageRepository.GetByIdAsync(request.ImageId);
            if (imageToRemove == null)
            {
                throw new EntityNotFoundException($"Image with ID {request.ImageId} is not found!");
            }

            if (imageToRemove.EntityId == request.HotelId)
            {
                await _imageRepository.DeleteAsync(request.ImageId);
                await _imageRepository.SaveChangesAsync();

                LogDeletionInformation(request.ImageId, request.HotelId);
            }
            else
                throw new EntityNotFoundException($"Hotel with ID {request.HotelId} does not " +
                                                    $"have an Image with ID {request.ImageId}!");
        }

        private void LogDeletionInformation(Guid imageId, Guid hotelId)
        {
            Console.WriteLine($"Image with ID {imageId} deleted from HotelId: {hotelId}");
        }
    }
}
