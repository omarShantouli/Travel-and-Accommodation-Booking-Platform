using Application.Commands;
using Application.Commands.Hotel_Commands;
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

namespace Application.Handlers.Hotel_Handler
{
    public class AddImageToHotelCommandHandler : IRequestHandler<AddImageToHotelCommand>
    {
        private readonly IRepository<Images> _imageRepository;
        private readonly IMapper _mapper;

        public AddImageToHotelCommandHandler(IRepository<Images> imageRepository, IMapper mapper)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;
        }

        public async Task Handle(AddImageToHotelCommand request, CancellationToken cancellationToken)
        {
            var image = _mapper.Map<Images>(request.Image);
            image.EntityType = EntityType.Hotel.ToString();
            image.EntityId = request.HotelId;

            await _imageRepository.CreateAsync(image);
            await _imageRepository.SaveChangesAsync();
        }
    }
}
