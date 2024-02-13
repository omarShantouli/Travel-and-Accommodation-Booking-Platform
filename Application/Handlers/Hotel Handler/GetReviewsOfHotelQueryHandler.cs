using Application.DTOs;
using Application.Queries;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers
{
    /// <summary>
    /// Handles the query to get reviews of a hotel.
    /// </summary>
    public class GetReviewsOfHotelQueryHandler : IRequestHandler<GetReviewsOfHotelQuery, List<ReviewDto>>
    {
        private readonly IRepository<Rooms> _roomRepository;
        private readonly IRepository<Reviews> _reviewsRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetReviewsOfHotelQueryHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetReviewsOfHotelQueryHandler"/> class.
        /// </summary>
        /// <param name="roomRepository">The repository for room entities.</param>
        /// <param name="reviewsRepository">The repository for review entities.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        public GetReviewsOfHotelQueryHandler(IRepository<Rooms> roomRepository,
               IRepository<Reviews> reviewsRepository, IMapper mapper, ILogger<GetReviewsOfHotelQueryHandler> logger)
        {
            _roomRepository = roomRepository;
            _reviewsRepository = reviewsRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Handles the query to get reviews of a hotel.
        /// </summary>
        /// <param name="request">The query request containing hotel information.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of review DTOs.</returns>
        public async Task<List<ReviewDto>> Handle(GetReviewsOfHotelQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var bookingIds = _roomRepository.GetAll().Where(r => r.HotelId == request.HotelId)
                    .SelectMany(room => room.Bookings).Select(b => b.Id).ToList();

                var reviews = _reviewsRepository.GetAll();

                var hotelReviews = reviews.Where(r => bookingIds.Contains(r.BookingId)).ToList();

                var reviewsDto = _mapper.Map<List<ReviewDto>>(hotelReviews);

                _logger.LogInformation($"Reviews of HotelId: {request.HotelId} - Count: {reviewsDto.Count}");

                return reviewsDto;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"An error occurred while handling GetReviewsOfHotelQuery: {ex.Message}");
                throw;
            }
        }
    }
}
