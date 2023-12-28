using Domain.Entities;
using Domain.Exceptions;
using static Domain.Interfaces.IRepository;

namespace Infrastructure.Data.Repositories
{
    public class ReviewsRepository : IRepository<Reviews>, IDisposable
    {
        private readonly ApplicationDbContext _context;

        public ReviewsRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Reviews GetById(Guid id)
        {
            var review = _context.Reviews.FirstOrDefault(r => r.Id == id);

            if (review == null)
            {
                throw new EntityNotFoundException($"Review with ID {id} not found.");
            }

            return review;
        }

        public IEnumerable<Reviews> GetAll()
        {
            return _context.Reviews;
        }

        public void Create(Reviews review)
        {
            if (review == null)
            {
                throw new ArgumentNullException(nameof(review));
            }

            _context.Reviews.Add(review);
            _context.SaveChanges();
        }

        public void Update(Reviews review)
        {
            if (review == null)
            {
                throw new ArgumentNullException(nameof(review));
            }

            var existingReview = _context.Reviews.FirstOrDefault(r => r.Id == review.Id);

            if (existingReview == null)
            {
                throw new EntityNotFoundException($"Review with ID {review.Id} not found.");
            }

            existingReview.BookingId = review.BookingId;
            existingReview.Comment = review.Comment;
            existingReview.ReviewDate = review.ReviewDate;
            existingReview.Rating = review.Rating;

            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var reviewToRemove = _context.Reviews.FirstOrDefault(r => r.Id == id);

            if (reviewToRemove == null)
            {
                throw new EntityNotFoundException($"Review with ID {id} not found.");
            }

            _context.Reviews.Remove(reviewToRemove);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
