using Application.Commands.Hotel_Commands;
using Castle.Core.Logging;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using static Domain.Interfaces.IRepository;
using static System.Net.Mime.MediaTypeNames;

namespace Application.Handlers.Hotel_Handler
{
    /// <summary>
    /// Handles the command to delete an image from a hotel.
    /// </summary>
    public class DeleteImageFromHotelCommandHandler : IRequestHandler<DeleteImageFromHotelCommand>
    {
        private readonly IRepository<Images> _imageRepository;
        private readonly ILogger<DeleteImageFromHotelCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteImageFromHotelCommandHandler"/> class.
        /// </summary>
        /// <param name="imageRepository">The repository for image entities.</param>
        public DeleteImageFromHotelCommandHandler(IRepository<Images> imageRepository, ILogger<DeleteImageFromHotelCommandHandler> logger)
        {
            _imageRepository = imageRepository;
            _logger = logger;
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

                _logger.LogInformation($"Image with ID {request.ImageId} deleted from HotelId: {request.HotelId}");
            }
            else
                throw new EntityNotFoundException($"Hotel with ID {request.HotelId} does not " +
                                                    $"have an Image with ID {request.ImageId}!");
        }

       
    }
}
