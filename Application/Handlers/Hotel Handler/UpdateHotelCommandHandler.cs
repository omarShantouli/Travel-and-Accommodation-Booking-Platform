using Application.Commands.Hotel_Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers.Hotel_Handler
{
    /// <summary>
    /// Handles the command to update a hotel entity.
    /// </summary>
    public class UpdateHotelCommandHandler : IRequestHandler<UpdateHotelCommand>
    {
        private readonly IRepository<Hotels> _hotelRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateHotelCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateHotelCommandHandler"/> class.
        /// </summary>
        /// <param name="hotelRepository">The repository for hotel entities.</param>
        /// <param name="mapper">The AutoMapper for mapping entities to DTOs.</param>
        /// <param name="logger">The logger for logging messages.</param>
        public UpdateHotelCommandHandler(IRepository<Hotels> hotelRepository, IMapper mapper, ILogger<UpdateHotelCommandHandler> logger)
        {
            _hotelRepository = hotelRepository ?? throw new ArgumentNullException(nameof(hotelRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the command to update a hotel entity.
        /// </summary>
        /// <param name="request">The command request containing the HotelId and UpdatedHotel.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async Task Handle(UpdateHotelCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Handling UpdateHotelCommand for HotelId: {request.HotelId}");

                var existingHotel = await _hotelRepository.GetByIdAsync(request.HotelId);

                if (existingHotel == null)
                {
                    _logger.LogWarning($"Hotel with ID {request.HotelId} not found.");
                    throw new EntityNotFoundException($"Hotel with ID {request.HotelId} not found.");
                }

                _mapper.Map(request.UpdatedHotel, existingHotel);

                await _hotelRepository.SaveChangesAsync();

                _logger.LogInformation($"UpdateHotelCommand handled successfully for HotelId: {request.HotelId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while handling UpdateHotelCommand for HotelId: {request.HotelId}");
                throw;
            }
        }
    }
}
