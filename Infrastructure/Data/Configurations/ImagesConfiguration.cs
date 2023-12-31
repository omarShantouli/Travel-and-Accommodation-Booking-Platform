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
    public class ImagesConfiguration : IEntityTypeConfiguration<Images>
    {
        public void Configure(EntityTypeBuilder<Images> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.EntityId)
                .IsRequired();

            builder.Property(i => i.EntityType)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(i => i.URL)
                .IsRequired()
                .HasMaxLength(1000); 

            builder.Property(i => i.Type)
                .IsRequired()
                .HasMaxLength(255);

        }
    }
}
