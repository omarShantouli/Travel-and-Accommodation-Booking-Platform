using Application.Commands.Rooms_Commands;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers.Rooms_Handler
{
    public class DeleteImageFromRoomCommandHandler : IRequestHandler<DeleteImageFromRoomCommand>
    {
        private readonly IRepository<Images> _imageRepository;

        public DeleteImageFromRoomCommandHandler(IRepository<Images> imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public async Task Handle(DeleteImageFromRoomCommand request, CancellationToken cancellationToken)
        {
            var imageToRemove = _imageRepository.GetById(request.ImageId);
            if (imageToRemove == null)
            {
                throw new EntityNotFoundException($"Image with ID {request.ImageId} is not found!");
            }

            if (imageToRemove.EntityId == request.RoomId)
            {
                _imageRepository.Delete(request.ImageId);

                await _imageRepository.SaveChangesAsync();
            }
            else
                throw new EntityNotFoundException($"Room with ID {request.RoomId} does not " +
                                                    $"have an Image with ID {request.ImageId}!");

        }
    }
}
