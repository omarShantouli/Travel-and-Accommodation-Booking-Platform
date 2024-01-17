using Application.Commands.Hotel_Commands;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers.Hotel_Handler
{
    /// <summary>
    /// Handles the command to apply partial updates to a hotel entity.
    /// </summary>
    public class PatchHotelCommandHandler : IRequestHandler<PatchHotelCommand>
    {
        private readonly IRepository<Hotels> _hotelRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PatchHotelCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PatchHotelCommandHandler"/> class.
        /// </summary>
        /// <param name="hotelRepository">The repository for hotel entities.</param>
        /// <param name="mapper">The AutoMapper for mapping entities to DTOs.</param>
        /// <param name="logger">The logger for logging messages.</param>
        public PatchHotelCommandHandler(IRepository<Hotels> hotelRepository, IMapper mapper, ILogger<PatchHotelCommandHandler> logger)
        {
            _hotelRepository = hotelRepository ?? throw new ArgumentNullException(nameof(hotelRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the command to apply partial updates to a hotel entity.
        /// </summary>
        /// <param name="request">The command request containing the HotelId and patchDocument.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async Task Handle(PatchHotelCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Handling PatchHotelCommand for HotelId: {request.HotelId}");

                var hotel = await _hotelRepository.GetByIdAsync(request.HotelId);

                if (hotel == null)
                {
                    _logger.LogWarning($"Hotel with ID {request.HotelId} not found.");
                    throw new EntityNotFoundException($"Hotel with ID {request.HotelId} not found.");
                }

                var hotelDto = _mapper.Map<HotelDto>(hotel);

                request.patchDocument.ApplyTo(hotelDto);

                _mapper.Map(hotelDto, hotel);

                await _hotelRepository.SaveChangesAsync();

                _logger.LogInformation($"PatchHotelCommand handled successfully for HotelId: {request.HotelId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while handling PatchHotelCommand for HotelId: {request.HotelId}");
                throw;
            }
        }
    }
}
