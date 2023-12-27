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
    public class RoomsConfiguration : IEntityTypeConfiguration<Rooms>
    {
        public void Configure(EntityTypeBuilder<Rooms> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.HotelId)
                .IsRequired();

            builder.Property(r => r.RoomTypeId)
                .IsRequired();

            builder.Property(r => r.AdultsCapacity)
                .IsRequired();

            builder.Property(r => r.ChildrenCapacity)
                .IsRequired();

            builder.Property(r => r.View)
                .HasMaxLength(255);

            builder.Property(r => r.Rating)
                .IsRequired();
        }
    }
}
