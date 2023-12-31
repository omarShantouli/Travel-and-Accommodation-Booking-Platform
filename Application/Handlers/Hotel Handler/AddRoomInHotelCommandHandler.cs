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
        private readonly IMapper _mapper;

        public AddRoomInHotelCommandHandler(IRepository<Hotels> hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task Handle(AddRoomInHotelCommand request, CancellationToken cancellationToken)
        {
            var hotel = _hotelRepository.GetById(request.HotelId);
            if (hotel == null)
            {
                throw new EntityNotFoundException($"Hotel with ID {request.HotelId} not found.");
            }

            var room = _mapper.Map<Rooms>(request.Room);

            hotel.Rooms.Add(room);
            await _hotelRepository.SaveChangesAsync();
        }
    }
}
