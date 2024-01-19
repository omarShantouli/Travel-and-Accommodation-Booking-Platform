using Application.Commands.Rooms_Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers.Rooms_Handler
{
    /// <summary>
    /// Handles the command to create a new room.
    /// </summary>
    public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand> 
    {
        private readonly IRepository<Rooms> _roomRepository; 
        private readonly IMapper _mapper;
        private readonly ILogger<CreateRoomCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateRoomCommandHandler"/> class.
        /// </summary>
        /// <param name="roomRepository">The repository for room entities.</param>  
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing and logging information related to CreateRoomCommandHandler.</param>
        public CreateRoomCommandHandler(IRepository<Rooms> roomRepository, IMapper mapper, ILogger<CreateRoomCommandHandler> logger)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Handles the command to create a new room.
        /// </summary>
        /// <param name="request">The command request containing room information.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public async Task Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Room == null)
                {
                    _logger.LogError("Room object is null.");
                    throw new EntityNotFoundException("Room object is null.");
                }

                var roomToAdd = _mapper.Map<Rooms>(request.Room);
                await _roomRepository.CreateAsync(roomToAdd);
                await _roomRepository.SaveChangesAsync();

                _logger.LogInformation($"Room created successfully. RoomId: {roomToAdd.Id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while handling CreateRoomCommand.");
                throw;
            }
        }
    }
}
