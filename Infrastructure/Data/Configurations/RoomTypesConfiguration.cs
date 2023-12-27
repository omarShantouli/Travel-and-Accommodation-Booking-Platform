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
    public class RoomTypesConfiguration : IEntityTypeConfiguration<RoomTypes>
    {
        public void Configure(EntityTypeBuilder<RoomTypes> builder)
        {
            builder.HasKey(rt => rt.Id);

            builder.Property(rt => rt.HotelId)
                .IsRequired();

            builder.Property(rt => rt.Type)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(rt => rt.PricePerNight)
                .IsRequired();
        }
    }
}
