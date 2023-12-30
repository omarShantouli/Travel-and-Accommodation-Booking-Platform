using Application.Commands;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers
{
    public class DeleteImageFromCityCommandHandler : IRequestHandler<DeleteImageFromCityCommand>
    {
        private readonly IRepository<Images> _imageRepository;

        public DeleteImageFromCityCommandHandler(IRepository<Images> imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public async Task Handle(DeleteImageFromCityCommand request, CancellationToken cancellationToken)
        {
            var imageToRemove = _imageRepository.GetById(request.ImageId);
            if (imageToRemove == null)
            {
                throw new EntityNotFoundException($"Image with ID {request.ImageId} is not found!");
            }
            else
            {
                _imageRepository.Delete(request.ImageId);

                await _imageRepository.SaveChangesAsync();
            }
            
        }

    }
}
