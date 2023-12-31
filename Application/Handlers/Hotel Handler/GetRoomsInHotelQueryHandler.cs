using Application.DTOs;
using Application.Queries;
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
    public class GetRoomsInHotelQueryHandler : IRequestHandler<GetRoomsInHotelQuery, List<RoomDto>>
    {
        public readonly IRepository<Hotels> _hotelRepository;
        public readonly IMapper _mapper;

        public GetRoomsInHotelQueryHandler(IRepository<Hotels> hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task<List<RoomDto>> Handle(GetRoomsInHotelQuery request, CancellationToken cancellationToken)
        {
           var hotel = _hotelRepository.GetById(request.HotelId);
            if(hotel == null)
            {
                throw new EntityNotFoundException($"Hotel with ID {request.HotelId} not found.");
            }

            var rooms = hotel.Rooms;
            var roomsDto = _mapper.Map<List<RoomDto>>(rooms);

            return roomsDto;

        }
    }
}
