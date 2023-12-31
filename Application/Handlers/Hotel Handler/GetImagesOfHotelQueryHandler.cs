using Application.DTOs;
using Application.Queries;
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
    public class GetImagesOfHotelQueryHandler : IRequestHandler<GetImagesOfHotelQuery, List<ImageDto>>
    {
        public readonly IRepository<Images> _imageRepository;
        public readonly IMapper _mapper;

        public GetImagesOfHotelQueryHandler(IRepository<Images> imageRepository, IMapper mapper)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;
        }

        public async Task<List<ImageDto>> Handle(GetImagesOfHotelQuery request, CancellationToken cancellationToken)
        {
            var images = _imageRepository.GetAll();

            var hotelImages = images.Where(image => image.EntityId == request.HotelId 
                                          && image.EntityType == EntityType.Hotel.ToString());

            var imagesDtos = _mapper.Map<List<ImageDto>>(hotelImages);

            return imagesDtos;
        }
    }
}
