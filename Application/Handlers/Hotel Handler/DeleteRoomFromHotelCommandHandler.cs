using Application.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers
{
    /// <summary>
    /// Handles the command to delete a room from a hotel.
    /// </summary>
    public class DeleteRoomFromHotelCommandHandler : IRequestHandler<DeleteRoomFromHotelCommand>
    {
        private readonly IRepository<Hotels> _hotelRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteRoomFromHotelCommandHandler"/> class.
        /// </summary>
        /// <param name="hotelRepository">The repository for hotel entities.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        public DeleteRoomFromHotelCommandHandler(IRepository<Hotels> hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        /// <summary>
        /// Handles the command to delete a room from a hotel.
        /// </summary>
        /// <param name="request">The command request containing hotel and room information.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public async Task Handle(DeleteRoomFromHotelCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var hotel = await _hotelRepository.GetByIdAsync(request.HotelId);
                if (hotel == null)
                {
                    throw new EntityNotFoundException($"Hotel with ID {request.HotelId} not found.");
                }

                var roomToRemove = hotel.Rooms.FirstOrDefault(r => r.Id == request.RoomId);
                if (roomToRemove == null)
                {
                    throw new EntityNotFoundException($"Room with ID {request.RoomId} not found in Hotel with ID {request.HotelId}.");
                }

                hotel.Rooms.Remove(roomToRemove);

                await _hotelRepository.SaveChangesAsync();

                LogDeletionInformation(request.RoomId, request.HotelId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while handling DeleteRoomFromHotelCommand: {ex.Message}");
                throw;
            }
        }

        private void LogDeletionInformation(Guid roomId, Guid hotelId)
        {
            Console.WriteLine($"Room with ID {roomId} deleted from HotelId: {hotelId}");
        }
    }
}
