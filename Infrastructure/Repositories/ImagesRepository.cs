using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using static Domain.Interfaces.IRepository;

namespace Infrastructure.Repositories
{
    public class ImagesRepository : IRepository<Images>, IDisposable
    {
        private readonly ApplicationDbContext _context;

        public ImagesRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Images> GetByIdAsync(Guid id)
        {
            var image = await _context.Images.FirstOrDefaultAsync(i => i.Id == id);

            if (image == null)
            {
                throw new EntityNotFoundException($"Image with ID {id} not found.");
            }

            return image;
        }

        public IEnumerable<Images> GetAll()
        {
            return _context.Images;
        }

        public async Task<Images> CreateAsync(Images image)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            _context.Images.Add(image);
            await _context.SaveChangesAsync();
            return image;
        }

        public async Task UpdateAsync(Images image)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            var existingImage = await _context.Images.FirstOrDefaultAsync(i => i.Id == image.Id);

            if (existingImage == null)
            {
                throw new EntityNotFoundException($"Image with ID {image.Id} not found.");
            }

            existingImage.EntityId = image.EntityId;
            existingImage.EntityType = image.EntityType;
            existingImage.URL = image.URL;
            existingImage.Type = image.Type;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var imageToRemove = _context.Images.FirstOrDefault(i => i.Id == id);

            if (imageToRemove == null)
            {
                throw new EntityNotFoundException($"Image with ID {id} not found.");
            }

            _context.Images.Remove(imageToRemove);
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
