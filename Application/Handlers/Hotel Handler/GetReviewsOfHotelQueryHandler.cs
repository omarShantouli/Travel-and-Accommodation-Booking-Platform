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
        public readonly IRepository<Reviews> _reviewsRepository;
        public readonly IMapper _mapper;

        public GetReviewsOfHotelQueryHandler(IRepository<Rooms> roomRepository,
               IRepository<Reviews> reviewsRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _reviewsRepository = reviewsRepository;
            _mapper = mapper;
        }

        public async Task<List<ReviewDto>> Handle(GetReviewsOfHotelQuery request, CancellationToken cancellationToken)
        {
            var bookingIds = _roomRepository.GetAll().Where(r => r.HotelId == request.HotelId)
                .SelectMany(room => room.Bookings).Select(b => b.Id).ToList();

            var reviews = _reviewsRepository.GetAll();

            var hotelReviews = reviews.Where(r => bookingIds.Contains(r.BookingId) == true).ToList();

            var reviewsDto = _mapper.Map<List<ReviewDto>>(hotelReviews);

            return reviewsDto;
        }
    }
}
