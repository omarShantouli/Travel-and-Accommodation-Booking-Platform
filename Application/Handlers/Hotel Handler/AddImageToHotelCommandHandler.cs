using Application.Commands.Hotel_Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers.Hotel_Handler
{
    /// <summary>
    /// Handles the command to add an image to a hotel.
    /// </summary>
    public class AddImageToHotelCommandHandler : IRequestHandler<AddImageToHotelCommand>
    {
        private readonly IRepository<Images> _imageRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AddImageToHotelCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddImageToHotelCommandHandler"/> class.
        /// </summary>
        /// <param name="imageRepository">The repository for image entities.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing and logging information related to AddImageToHotelCommandHandler.</param>
        public AddImageToHotelCommandHandler(IRepository<Images> imageRepository, IMapper mapper, ILogger<AddImageToHotelCommandHandler> logger)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Handles the command to add an image to a hotel.
        /// </summary>
        /// <param name="request">The command request containing image and hotel information.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public async Task Handle(AddImageToHotelCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var image = _mapper.Map<Images>(request.Image);

                image.EntityType = EntityType.Hotel.ToString();
                image.EntityId = request.HotelId;

                await _imageRepository.CreateAsync(image);
                await _imageRepository.SaveChangesAsync();

                _logger.LogInformation($"Image added to HotelId: {request.HotelId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while handling AddImageToHotelCommand.");
                throw;
            }
        }
    }
}
