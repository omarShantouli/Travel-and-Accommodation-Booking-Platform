using Application.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using static Domain.Interfaces.IRepository;

namespace Application.Handlers
{
    public class AddImageToCityCommandHandler : IRequestHandler<AddImageToCityCommand>
    {
        private readonly IRepository<Images> _imageRepository;
        private readonly IMapper _mapper;

        public AddImageToCityCommandHandler(IRepository<Images> imageRepository, IMapper mapper)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;
        }

        public async Task Handle(AddImageToCityCommand request, CancellationToken cancellationToken)
        {
            var image = _mapper.Map<Images>(request.Image);
            image.EntityType = EntityType.City.ToString();
            image.EntityId = request.CityId;

            await _imageRepository.CreateAsync(image);
            await _imageRepository.SaveChangesAsync();
        }
    }
}
