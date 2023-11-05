using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoamI.Data.Entitys.Objects;

namespace WhoamI.Data.EntityFrameworkCore.Mappings
{
    public class TestimonialMap : IEntityTypeConfiguration<Testimonial>
    {
        public void Configure(EntityTypeBuilder<Testimonial> builder)
        {

            builder.ToTable("Testimonial");

            builder.HasKey(a => a.Id);

            builder.Property(u => u.CreationDate).HasColumnType("datetime2(7)").IsRequired(true);

            builder.Property(u => u.IsDeleted).HasColumnType("bit").IsRequired(true);

            builder.Property(a => a.Name).HasMaxLength(50).IsRequired(true);
            builder.Property(a => a.Surname).HasMaxLength(50).IsRequired(true);
            builder.Property(a => a.Job).HasMaxLength(50).IsRequired(false);
            builder.Property(a => a.Opinion).HasMaxLength(255).IsRequired(false);


            builder.Property(a => a.UserId).HasColumnType("int").IsRequired(true);

            builder.HasOne(a => a.User).WithMany(u => u.Testimonials).HasForeignKey(a => a.UserId);

        }
    }
}
