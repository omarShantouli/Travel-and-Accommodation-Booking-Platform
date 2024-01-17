using Application.Commands.Hotel_Commands;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers.Hotel_Handler
{

    /// <summary>
    /// Handles the command to delete a hotel.
    /// </summary>
    public class DeleteHotelCommandHandler : IRequestHandler<DeleteHotelCommand>
    {
        private readonly IRepository<Hotels> _hotelRepository;
        private readonly ILogger<DeleteHotelCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteHotelCommandHandler"/> class.
        /// </summary>
        /// <param name="hotelRepository">The repository for hotel entities.</param>
        /// <param name="logger">The logger for capturing and logging information related to DeleteHotelCommandHandler.</param>
        public DeleteHotelCommandHandler(IRepository<Hotels> hotelRepository, ILogger<DeleteHotelCommandHandler> logger)
        {
            _hotelRepository = hotelRepository;
            _logger = logger;
        }

        /// <summary>
        /// Handles the command to delete a hotel.
        /// </summary>
        /// <param name="request">The command request containing hotel ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public async Task Handle(DeleteHotelCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Handling DeleteHotelCommand for HotelId: {request.HotelId}");

                var existingHotel = await _hotelRepository.GetByIdAsync(request.HotelId);

                if (existingHotel == null)
                {
                    _logger.LogWarning($"Hotel with ID {request.HotelId} not found.");
                    throw new EntityNotFoundException($"Hotel with ID {request.HotelId} not found.");
                }

                await _hotelRepository.DeleteAsync(existingHotel.Id);

                await _hotelRepository.SaveChangesAsync();

                _logger.LogInformation($"DeleteHotelCommand handled successfully for HotelId: {request.HotelId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while handling DeleteHotelCommand for HotelId: {request.HotelId}");
                throw;
            }
        }
    }
}
