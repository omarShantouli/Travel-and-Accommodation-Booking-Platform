using Domain.Entities;
using BCrypt.Net;

namespace Infrastructure.Data.Entities_Seeding
{
    public class AppUserSeeding
    {
        public static IEnumerable<AppUser> SeedData()
        {
            return new List<AppUser>
            {
                new AppUser
                {
                    Id = Guid.NewGuid(),
                    Email = "admin@example.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("123admin", salt: "$2a$12$6FjKqlXYrjM/oHxRpmHGSu"),
                    Role = "Admin"
                },
                new AppUser
                {
                    Id = Guid.NewGuid(),
                    Email = "john.doe@example.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("123john", salt: "$2a$12$6FjKqlXYrjM/oHxRpmHGSu"),
                    Role = "User"
                },
                new AppUser
                {
                    Id = Guid.NewGuid(),
                    Email = "jane.smith@example.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("123jane", salt : "$2a$12$6FjKqlXYrjM/oHxRpmHGSu"),
                    Role = "User"
                }
            };
        }
    }
}
