using Domain.Entities;
using Domain.Exceptions;
using static Domain.Interfaces.IRepository;

namespace Infrastructure.Data.Repositories
{
    public class ImagesRepository : IRepository<Images>, IDisposable
    {
        private readonly ApplicationDbContext _context;

        public ImagesRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Images GetById(Guid id)
        {
            var image = _context.Images.FirstOrDefault(i => i.Id == id);

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

        public void Create(Images image)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            _context.Images.Add(image);
            _context.SaveChanges();
        }

        public void Update(Images image)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            var existingImage = _context.Images.FirstOrDefault(i => i.Id == image.Id);

            if (existingImage == null)
            {
                throw new EntityNotFoundException($"Image with ID {image.Id} not found.");
            }

            existingImage.EntityId = image.EntityId;
            existingImage.EntityType = image.EntityType;
            existingImage.URL = image.URL;
            existingImage.Type = image.Type;

            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var imageToRemove = _context.Images.FirstOrDefault(i => i.Id == id);

            if (imageToRemove == null)
            {
                throw new EntityNotFoundException($"Image with ID {id} not found.");
            }

            _context.Images.Remove(imageToRemove);
            _context.SaveChanges();
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
