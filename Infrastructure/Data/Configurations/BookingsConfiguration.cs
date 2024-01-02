using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configurations
{
    public class BookingsConfiguration : IEntityTypeConfiguration<Bookings>
    {
        public void Configure(EntityTypeBuilder<Bookings> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.CheckInDate)
                .IsRequired();

            builder.Property(b => b.CheckOutDate)
                .IsRequired();

            builder.Property(b => b.BookingDate)
                .IsRequired();

        }
    }
}
