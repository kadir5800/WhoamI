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
    public class EducationMap : IEntityTypeConfiguration<Education>
    {
        public void Configure(EntityTypeBuilder<Education> builder)
        {

            builder.ToTable("Education");

            builder.HasKey(a => a.Id);

            builder.Property(u => u.CreationDate).HasColumnType("datetime2(7)").IsRequired(true);
            builder.Property(u => u.StartDate).HasColumnType("datetime2(7)").IsRequired(true);
            builder.Property(u => u.EndDate).HasColumnType("datetime2(7)").IsRequired(true);

            builder.Property(u => u.IsDeleted).HasColumnType("bit").IsRequired(true);
            builder.Property(u => u.IsRunning).HasColumnType("bit").IsRequired(true);

            builder.Property(a => a.Degree).HasMaxLength(50).IsRequired(true);
            builder.Property(a => a.School).HasMaxLength(100).IsRequired(true);


            builder.Property(a => a.UserId).HasColumnType("int").IsRequired(true);

            builder.HasOne(a => a.User).WithMany(u => u.Educations).HasForeignKey(a => a.UserId);

        }
    }
}
