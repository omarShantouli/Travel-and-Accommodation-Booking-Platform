using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using static Domain.Interfaces.IRepository;

namespace Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {

            services.AddScoped<IRepository<City>, CityRepository>();
            services.AddScoped<IRepository<Hotels>, HotelsRepository>();
            services.AddScoped<IRepository<Rooms>, RoomsRepository>();
            services.AddScoped<IRepository<Images>, ImagesRepository>();
            services.AddScoped<IRepository<Bookings>, BookingsRepository>();
            services.AddScoped<IRepository<AppUser>, AppUserRepository>();
            services.AddScoped<IRepository<Owner>, OwnerRepository>();
            services.AddScoped<IRepository<RoomTypes>, RoomTypesRepository>();
            services.AddScoped<IRepository<Guest>, GuestRepository>();
            services.AddScoped<IRepository<Reviews>, ReviewsRepository>();
            services.AddScoped<IRepository<AppUser>, AppUserRepository>();

            return services;
        }
    }
}
