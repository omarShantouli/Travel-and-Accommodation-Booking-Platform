using Application.Commands;
using Application.Commands.Hotel_Commands;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers.Hotel_Handler
{
    public class DeleteImageFromHotelCommandHandler : IRequestHandler<DeleteImageFromHotelCommand>
    {
        private readonly IRepository<Images> _imageRepository;

        public DeleteImageFromHotelCommandHandler(IRepository<Images> imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public async Task Handle(DeleteImageFromHotelCommand request, CancellationToken cancellationToken)
        {
            var imageToRemove = _imageRepository.GetById(request.ImageId);
            if (imageToRemove == null)
            {
                throw new EntityNotFoundException($"Image with ID {request.ImageId} is not found!");
            }

            if(imageToRemove.EntityId == request.HotelId)
            {
                _imageRepository.Delete(request.ImageId);

                await _imageRepository.SaveChangesAsync();
            }
            else
                throw new EntityNotFoundException($"Hotel with ID {request.HotelId} does not " +
                                                    $"have an Image with ID {request.ImageId}!");


        }

    }
}
