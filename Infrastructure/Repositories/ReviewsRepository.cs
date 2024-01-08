using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using static Domain.Interfaces.IRepository;

namespace Infrastructure.Repositories
{
    public class ReviewsRepository : IRepository<Reviews>, IDisposable
    {
        private readonly ApplicationDbContext _context;

        public ReviewsRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Reviews> GetByIdAsync(Guid id)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id);

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

        public async Task<Reviews> CreateAsync(Reviews review)
        {
            if (review == null)
            {
                throw new ArgumentNullException(nameof(review));
            }

            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task UpdateAsync(Reviews review)
        {
            if (review == null)
            {
                throw new ArgumentNullException(nameof(review));
            }

            var existingReview = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == review.Id);

            if (existingReview == null)
            {
                throw new EntityNotFoundException($"Review with ID {review.Id} not found.");
            }

            existingReview.BookingId = review.BookingId;
            existingReview.Comment = review.Comment;
            existingReview.ReviewDate = review.ReviewDate;
            existingReview.Rating = review.Rating;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var reviewToRemove = _context.Reviews.FirstOrDefault(r => r.Id == id);

            if (reviewToRemove == null)
            {
                throw new EntityNotFoundException($"Review with ID {id} not found.");
            }

            _context.Reviews.Remove(reviewToRemove);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
