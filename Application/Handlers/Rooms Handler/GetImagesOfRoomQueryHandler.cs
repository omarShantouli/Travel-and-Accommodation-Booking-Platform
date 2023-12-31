using Application.DTOs;
using Application.Queries;
using Application.Queries.Rooms_Queries;
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

namespace Application.Handlers.GetImagesOfRoomQueryHandler
{
    public class GetImagesOfRoomQueryHandler : IRequestHandler<GetImagesOfRoomQuery, List<ImageDto>>
    {
        public readonly IRepository<Images> _imageRepository;
        public readonly IMapper _mapper;

        public GetImagesOfRoomQueryHandler(IRepository<Images> imageRepository, IMapper mapper)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;
        }

        public async Task<List<ImageDto>> Handle(GetImagesOfRoomQuery request, CancellationToken cancellationToken)
        {
            var images = _imageRepository.GetAll();

            var roomImages = images.Where(image => image.EntityId == request.RoomId
                                          && image.EntityType == EntityType.Room.ToString());

            var imagesDtos = _mapper.Map<List<ImageDto>>(roomImages);

            return imagesDtos;
        }
    }
}
