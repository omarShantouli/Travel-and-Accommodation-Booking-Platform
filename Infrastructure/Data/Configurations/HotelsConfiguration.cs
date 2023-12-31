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
    public class HotelsConfiguration : IEntityTypeConfiguration<Hotels>
    {
        public void Configure(EntityTypeBuilder<Hotels> builder)
        {
            builder.HasKey(h => h.Id);

            builder.Property(h => h.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(h => h.Rating)
                .IsRequired();

            builder.Property(h => h.StreetAddress)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(h => h.Description)
                .HasMaxLength(1000);

            builder.Property(h => h.Phone)
                .HasMaxLength(25);

            builder.Property(h => h.FloorsNumber)
                .IsRequired();

            builder.HasOne(h => h.City)
                .WithMany()
                .HasForeignKey(h => h.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(h => h.Owner)
                .WithMany()
                .HasForeignKey(h => h.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
