using Application.Commands.Rooms_Commands;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers.Rooms_Handler
{
    /// <summary>
    /// Handles the command to apply partial updates to a room entity.
    /// </summary>
    public class PatchRoomCommandHandler : IRequestHandler<PatchRoomCommand>  // Adjust based on your actual command
    {
        private readonly IRepository<Rooms> _roomRepository;  // Adjust based on your actual entity
        private readonly IMapper _mapper;
        private readonly ILogger<PatchRoomCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PatchRoomCommandHandler"/> class.
        /// </summary>
        /// <param name="roomRepository">The repository for room entities.</param>  // Adjust based on your actual entity
        /// <param name="mapper">The AutoMapper for mapping entities to DTOs.</param>
        /// <param name="logger">The logger for logging messages.</param>
        public PatchRoomCommandHandler(IRepository<Rooms> roomRepository, IMapper mapper, ILogger<PatchRoomCommandHandler> logger)
        {
            _roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the command to apply partial updates to a room entity.
        /// </summary>
        /// <param name="request">The command request containing the RoomId and patchDocument.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async Task Handle(PatchRoomCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Handling PatchRoomCommand for RoomId: {request.RoomId}");  // Adjust based on your actual command

                var room = await _roomRepository.GetByIdAsync(request.RoomId);  // Adjust based on your actual entity

                if (room == null)
                {
                    _logger.LogWarning($"Room with ID {request.RoomId} not found.");  // Adjust based on your actual entity
                    throw new EntityNotFoundException($"Room with ID {request.RoomId} not found.");  // Adjust based on your actual entity
                }

                var roomDto = _mapper.Map<RoomDto>(room);

                request.patchDocument.ApplyTo(roomDto);

                _mapper.Map(roomDto, room);

                await _roomRepository.SaveChangesAsync();

                _logger.LogInformation($"PatchRoomCommand handled successfully for RoomId: {request.RoomId}");  // Adjust based on your actual command
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while handling PatchRoomCommand for RoomId: {request.RoomId}");  // Adjust based on your actual command
                throw;
            }
        }
    }
}
