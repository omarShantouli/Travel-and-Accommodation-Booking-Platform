using Domain.Entities;
using Infrastructure.Data.Entities_Seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Nest.Specification.MigrationApi;
using System.Reflection;


namespace Infrastructure.Data
{
    [DbContext(typeof(ApplicationDbContext))]

    public class ApplicationDbContext : DbContext
    {
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Bookings> Bookings { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Hotels> Hotels { get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Payments> Payments { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<Rooms> Rooms { get; set; }
        public DbSet<RoomTypes> RoomTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=OMAR\\SQLEXPRESS;Database=TravelBookingDB;Trusted_Connection=True;TrustServerCertificate=True;");

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);

            // Seeding Entities
            builder.Entity<Bookings>().HasData(BookingsSeeding.SeedData());
            builder.Entity<City>().HasData(CitySeeding.SeedData());
            builder.Entity<Guest>().HasData(GuestSeeding.SeedData());
            builder.Entity<Hotels>().HasData(HotelsSeeding.SeedData());
            builder.Entity<Owner>().HasData(OwnerSeeding.SeedData());
            builder.Entity<Payments>().HasData(PaymentsSeeding.SeedData());
            builder.Entity<Reviews>().HasData(ReviewsSeeding.SeedData());
            builder.Entity<Rooms>().HasData(RoomsSeeding.SeedData());
            builder.Entity<RoomTypes>().HasData(RoomTypesSeeding.SeedData());
        }
    }
}
