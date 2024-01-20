using Application.Commands.RoomType_Commands;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers.RoomType_Handler
{
    /// <summary>
    /// Handles the command to apply partial updates to a room type entity.
    /// </summary>
    public class PatchRoomTypeCommandHandler : IRequestHandler<PatchRoomTypeCommand>
    {
        private readonly IRepository<RoomTypes> _roomTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PatchRoomTypeCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PatchRoomTypeCommandHandler"/> class.
        /// </summary>
        /// <param name="roomTypeRepository">The repository for room type entities.</param>
        /// <param name="mapper">The AutoMapper for mapping entities to DTOs.</param>
        /// <param name="logger">The logger for logging messages.</param>
        public PatchRoomTypeCommandHandler(IRepository<RoomTypes> roomTypeRepository, IMapper mapper, ILogger<PatchRoomTypeCommandHandler> logger)
        {
            _roomTypeRepository = roomTypeRepository ?? throw new ArgumentNullException(nameof(roomTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the command to apply partial updates to a room type entity.
        /// </summary>
        /// <param name="request">The command request containing the RoomTypeId and patchDocument.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async Task Handle(PatchRoomTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Handling PatchRoomTypeCommand for RoomTypeId: {request.RoomTypeId}");

                var roomType = await _roomTypeRepository.GetByIdAsync(request.RoomTypeId);

                if (roomType == null)
                {
                    _logger.LogWarning($"Room type with ID {request.RoomTypeId} not found.");
                    throw new EntityNotFoundException($"Room type with ID {request.RoomTypeId} not found.");
                }

                var roomTypeDto = _mapper.Map<RoomTypeDto>(roomType);

                request.patchDocument.ApplyTo(roomTypeDto);

                _mapper.Map(roomTypeDto, roomType);

                await _roomTypeRepository.SaveChangesAsync();

                _logger.LogInformation($"PatchRoomTypeCommand handled successfully for RoomTypeId: {request.RoomTypeId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while handling PatchRoomTypeCommand for RoomTypeId: {request.RoomTypeId}");
                throw;
            }
        }
    }
}
