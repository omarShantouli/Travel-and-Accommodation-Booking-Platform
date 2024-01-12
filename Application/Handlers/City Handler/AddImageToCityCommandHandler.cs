using Application.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers
{
    public class AddImageToCityCommandHandler : IRequestHandler<AddImageToCityCommand>
    {
        private readonly IRepository<Images> _imageRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AddImageToCityCommandHandler> _logger;

        public AddImageToCityCommandHandler(IRepository<Images> imageRepository, IMapper mapper, ILogger<AddImageToCityCommandHandler> logger)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Handles the command to add an image to a city.
        /// </summary>
        /// <param name="request">The command request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public async Task Handle(AddImageToCityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Handling AddImageToCityCommand for CityId: {request.CityId}");

                if (request.Image == null)
                {
                    _logger.LogError("Image data is null. Unable to process the command.");
                    throw new ArgumentNullException(nameof(request.Image), "Image data cannot be null.");
                }

                var image = _mapper.Map<Images>(request.Image);
                image.EntityType = EntityType.City.ToString();
                image.EntityId = request.CityId;

                await _imageRepository.CreateAsync(image);
                await _imageRepository.SaveChangesAsync();

                _logger.LogInformation($"AddImageToCityCommand handled successfully for CityId: {request.CityId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while handling AddImageToCityCommand for CityId: {request.CityId}");
                throw;
            }
        }
    }
}
