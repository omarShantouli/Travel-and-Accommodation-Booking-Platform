using Application.DTOs;
using Application.Handlers;
using Application.Queries;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using static Domain.Interfaces.IRepository;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            
            var assembly = typeof(DependencyInjection).Assembly;
            services.AddMediatR(configuration =>
                    configuration.RegisterServicesFromAssembly(assembly));
            services.AddValidatorsFromAssembly(assembly);
            services.AddTransient<IRequestHandler<GetHotelsInCityQuery, List<HotelDto>>, GetHotelsInCityQueryHandler>();

            return services;
        }
    }
}
