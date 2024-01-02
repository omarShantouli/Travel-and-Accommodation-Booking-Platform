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
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.CountryName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.PostOffice)
                .HasMaxLength(50);

            builder.Property(c => c.CountryCode)
                .IsRequired()
                .HasMaxLength(15);

        }
    }
}
