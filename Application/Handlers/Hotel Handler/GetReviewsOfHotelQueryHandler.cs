using Application.DTOs;
using Application.Queries;
using AutoMapper;
using Domain.Entities;
using MediatR;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers
{
    public class GetReviewsOfHotelQueryHandler : IRequestHandler<GetReviewsOfHotelQuery, List<ReviewDto>>
    {
        public readonly IRepository<Rooms> _roomRepository;
        public readonly IMapper _mapper;

        public GetReviewsOfHotelQueryHandler(IRepository<Rooms> roomRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

        public async Task<List<ReviewDto>> Handle(GetReviewsOfHotelQuery request, CancellationToken cancellationToken)
        {
            var rooms = _roomRepository.GetAll().Where(r => r.HotelId == request.HotelId);

            var reviews = rooms.Select(r => r.Bookings.Select(b => b.Review));

            var reviewsDto = _mapper.Map<List<ReviewDto>>(reviews);

            return reviewsDto;
        }
    }
}
