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
    /// Handles the command to create a new hotel.
    /// </summary>
    public class CreateHotelCommandHandler : IRequestHandler<CreateHotelCommand>
    {
        private readonly IRepository<Hotels> _hotelRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateHotelCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateHotelCommandHandler"/> class.
        /// </summary>
        /// <param name="hotelRepository">The repository for hotel entities.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing and logging information related to CreateHotelCommandHandler.</param>
        public CreateHotelCommandHandler(IRepository<Hotels> hotelRepository, IMapper mapper, ILogger<CreateHotelCommandHandler> logger)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Handles the command to create a new hotel.
        /// </summary>
        /// <param name="request">The command request containing hotel information.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public async Task Handle(CreateHotelCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Hotel == null)
                {
                    _logger.LogError("Hotel object is null.");
                    throw new EntityNotFoundException("Hotel object is null.");
                }

                var hotelToAdd = _mapper.Map<Hotels>(request.Hotel);
                await _hotelRepository.CreateAsync(hotelToAdd);
                await _hotelRepository.SaveChangesAsync();

                _logger.LogInformation($"Hotel created successfully. HotelId: {hotelToAdd.Id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while handling CreateHotelCommand.");
                throw;
            }
        }
    }
}
