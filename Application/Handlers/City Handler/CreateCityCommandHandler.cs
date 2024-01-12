using Application.Commands.City_Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using static Domain.Interfaces.IRepository;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.City_Handler
{
    /// <summary>
    /// Handles the command to create a new city.
    /// </summary>
    public class CreateCityCommandHandler : IRequestHandler<CreateCityCommand>
    {
        private readonly IRepository<City> _cityRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCityCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCityCommandHandler"/> class.
        /// </summary>
        /// <param name="cityRepository">The repository for city entities.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="logger">The logger for capturing and logging information related to CreateCityCommandHandler.</param>
        public CreateCityCommandHandler(IRepository<City> cityRepository, IMapper mapper, ILogger<CreateCityCommandHandler> logger)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Handles the command to create a new city.
        /// </summary>
        /// <param name="request">The command request containing city information.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public async Task Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.City == null)
                {
                    _logger.LogError("City object is null.");
                    throw new EntityNotFoundException("City object is null.");
                }

                var cityToAdd = _mapper.Map<City>(request.City);
                await _cityRepository.CreateAsync(cityToAdd);
                await _cityRepository.SaveChangesAsync();

                _logger.LogInformation($"City created successfully. CityId: {cityToAdd.Id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while handling CreateCityCommand.");
                throw;
            }
        }
    }
}
