using Application.DTOs;
using Application.Queries;
using AutoMapper;
using Domain.Entities;
using MediatR;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers
{
    public class GetAvailableRoomsInHotelQueryHandler : IRequestHandler<GetAvailableRoomsInHotelQuery, List<RoomDto>>
    {
        public readonly IRepository<Rooms> _roomRepository;
        public readonly IRepository<Bookings> _bookingRepository;
        public readonly IMapper _mapper;

        public GetAvailableRoomsInHotelQueryHandler(IRepository<Rooms> roomRepository,
                                                    IRepository<Bookings> bookingRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        public async Task<List<RoomDto>> Handle(GetAvailableRoomsInHotelQuery request, CancellationToken cancellationToken)
        {
           
            var bookedRoomIds = _bookingRepository.GetAll().Where(b => b.CheckOutDate >= DateTime.Now).
                                                                   Select(b => b.RoomId).ToList();
            Console.WriteLine("=================>  " + bookedRoomIds);
            var availableRooms = _roomRepository.GetAll().Where(r => r.HotelId == request.HotelId
                                                                && !bookedRoomIds.Contains(r.Id));
            
            var availableRoomsDto = _mapper.Map<List<RoomDto>>(availableRooms);

            return availableRoomsDto;
        }
    }
}
