using Application.Commands.Hotel_Commands;
using Application.Commands.Rooms_Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers.Rooms_Handler
{
    public class AddImageToRoomCommandHandler : IRequestHandler<AddImageToRoomCommand>
    {
        private readonly IRepository<Images> _imageRepository;
        private readonly IMapper _mapper;

        public AddImageToRoomCommandHandler(IRepository<Images> imageRepository, IMapper mapper)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;
        }

        public async Task Handle(AddImageToRoomCommand request, CancellationToken cancellationToken)
        {
            var image = _mapper.Map<Images>(request.Image);
            image.EntityType = EntityType.Room.ToString();
            image.EntityId = request.RoomId;

            _imageRepository.Create(image);
            await _imageRepository.SaveChangesAsync();
        }
    }
}
