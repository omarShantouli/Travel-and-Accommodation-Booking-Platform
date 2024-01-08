using Application.DTOs;
using Application.Handlers;
using Application.Queries;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            var assembly = typeof(ServiceCollectionExtensions).Assembly;
            services.AddMediatR(configuration =>
                    configuration.RegisterServicesFromAssembly(assembly));
            services.AddValidatorsFromAssembly(assembly);
            services.AddTransient<IRequestHandler<GetHotelsInCityQuery, List<HotelDto>>, GetHotelsInCityQueryHandler>();

            return services;
        }
    }
}
