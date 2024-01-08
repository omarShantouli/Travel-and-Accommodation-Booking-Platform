using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class AddUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(t => t.Email)
                .HasMaxLength(50).IsRequired();

            builder.Property(t => t.PasswordHash)
                .HasMaxLength(20).IsRequired();

            builder.Property(t => t.Role)
                .HasMaxLength(20).IsRequired();
        }
    }
}
