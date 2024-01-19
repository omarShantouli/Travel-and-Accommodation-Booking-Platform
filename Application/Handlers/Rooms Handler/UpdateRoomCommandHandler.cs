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
    /// Handles the command to update a room entity.
    /// </summary>
    public class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand>  // Adjust based on your actual command
    {
        private readonly IRepository<Rooms> _roomRepository;  // Adjust based on your actual entity
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateRoomCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateRoomCommandHandler"/> class.
        /// </summary>
        /// <param name="roomRepository">The repository for room entities.</param>  // Adjust based on your actual entity
        /// <param name="mapper">The AutoMapper for mapping entities to DTOs.</param>
        /// <param name="logger">The logger for logging messages.</param>
        public UpdateRoomCommandHandler(IRepository<Rooms> roomRepository, IMapper mapper, ILogger<UpdateRoomCommandHandler> logger)
        {
            _roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the command to update a room entity.
        /// </summary>
        /// <param name="request">The command request containing the RoomId and UpdatedRoom.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async Task Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Handling UpdateRoomCommand for RoomId: {request.RoomId}");  // Adjust based on your actual command

                var existingRoom = await _roomRepository.GetByIdAsync(request.RoomId);  // Adjust based on your actual entity

                if (existingRoom == null)
                {
                    _logger.LogWarning($"Room with ID {request.RoomId} not found.");  // Adjust based on your actual entity
                    throw new EntityNotFoundException($"Room with ID {request.RoomId} not found.");  // Adjust based on your actual entity
                }

                _mapper.Map(request.UpdatedRoom, existingRoom);

                await _roomRepository.SaveChangesAsync();

                _logger.LogInformation($"UpdateRoomCommand handled successfully for RoomId: {request.RoomId}");  // Adjust based on your actual command
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while handling UpdateRoomCommand for RoomId: {request.RoomId}");  // Adjust based on your actual command
                throw;
            }
        }
    }

}
