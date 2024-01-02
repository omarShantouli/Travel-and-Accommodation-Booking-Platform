using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configurations
{
    public class GuestConfiguration : IEntityTypeConfiguration<Guest>
    {
        public void Configure(EntityTypeBuilder<Guest> builder)
        {
            builder.HasKey(g => g.Id);

            builder.Property(g => g.FirstName)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(g => g.LastName)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(g => g.Email)
                .IsRequired()
                .HasMaxLength(50) 
                .IsUnicode(false)
                .HasColumnName("EmailAddress");

            builder.Property(g => g.Phone)
                .HasMaxLength(25);

        }
    }
}
