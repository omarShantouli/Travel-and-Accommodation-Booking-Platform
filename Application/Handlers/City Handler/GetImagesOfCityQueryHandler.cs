using Application.DTOs;
using Application.Queries;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers
{
    public class GetImagesOfCityQueryHandler : IRequestHandler<GetImagesOfCityQuery, List<ImageDto>>
    {
        public readonly IRepository<Images> _imageRepository;
        public readonly IMapper _mapper;

        public GetImagesOfCityQueryHandler(IRepository<Images> imageRepository, IMapper mapper)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;
        }

        public async Task<List<ImageDto>> Handle(GetImagesOfCityQuery request, CancellationToken cancellationToken)
        {
            var images = _imageRepository.GetAll();

            var cityImages = images.Where(image => image.EntityId == request.CityId
                                          && image.EntityType == EntityType.City.ToString());

            var imagesDtos = _mapper.Map<List<ImageDto>>(cityImages);

            return imagesDtos;
        }

    }
}
