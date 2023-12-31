using Application.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers
{
    public class DeleteRoomFromHotelCommandHandler : IRequestHandler<DeleteRoomFromHotelCommand>
    {
        private readonly IRepository<Hotels> _hotelRepository;
        private readonly IMapper _mapper;

        public DeleteRoomFromHotelCommandHandler(IRepository<Hotels> hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task Handle(DeleteRoomFromHotelCommand request, CancellationToken cancellationToken)
        {
            var hotel = _hotelRepository.GetById(request.HotelId);
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
        }
    }
}
