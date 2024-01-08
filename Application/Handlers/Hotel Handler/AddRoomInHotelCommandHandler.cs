using Application.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers
{
    public class AddRoomInHotelCommandHandler : IRequestHandler<AddRoomInHotelCommand>
    {
        private readonly IRepository<Hotels> _hotelRepository;
        private readonly IRepository<Rooms> _roomRepository;
        private readonly IMapper _mapper;

        public AddRoomInHotelCommandHandler(IRepository<Hotels> hotelRepository,
                                            IRepository<Rooms> roomRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

        public async Task Handle(AddRoomInHotelCommand request, CancellationToken cancellationToken)
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

        }
    }
}
