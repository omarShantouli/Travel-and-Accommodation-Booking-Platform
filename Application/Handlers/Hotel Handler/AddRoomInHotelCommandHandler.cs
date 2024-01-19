using Application.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers
{
    /// <summary>
    /// Handles the command to add a room to a hotel.
    /// </summary>
    public class AddRoomInHotelCommandHandler : IRequestHandler<AddRoomInHotelCommand>
    {
        private readonly IRepository<Hotels> _hotelRepository;
        private readonly IRepository<Rooms> _roomRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AddRoomInHotelCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddRoomInHotelCommandHandler"/> class.
        /// </summary>
        /// <param name="hotelRepository">The repository for hotel entities.</param>
        /// <param name="roomRepository">The repository for room entities.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing and logging information related to AddRoomInHotelCommandHandler.</param>
        public AddRoomInHotelCommandHandler(IRepository<Hotels> hotelRepository,
                                            IRepository<Rooms> roomRepository, IMapper mapper, ILogger<AddRoomInHotelCommandHandler> logger)
        {
            _hotelRepository = hotelRepository;
            _roomRepository = roomRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Handles the command to add a room to a hotel.
        /// </summary>
        /// <param name="request">The command request containing room and hotel information.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public async Task Handle(AddRoomInHotelCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var hotel = await _hotelRepository.GetByIdAsync(request.HotelId);
                if (hotel == null)
                {
                    throw new EntityNotFoundException($"Hotel with ID {request.HotelId} not found.");
                }

                var room = _mapper.Map<Rooms>(request.Room);

                await _roomRepository.CreateAsync(room);
                await _roomRepository.SaveChangesAsync();

                hotel.Rooms.Add(room);

                await _hotelRepository.SaveChangesAsync();

                _logger.LogInformation($"Room added to HotelId: {request.HotelId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while handling AddRoomInHotelCommand.");
                throw;
            }
        }
    }
}
