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
    public class ProjectMap : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {

            builder.ToTable("Project");

            builder.HasKey(a => a.Id);

            builder.Property(u => u.CreationDate).HasColumnType("datetime2(7)").IsRequired(true);

            builder.Property(u => u.IsDeleted).HasColumnType("bit").IsRequired(true);


            builder.Property(a => a.Name).HasMaxLength(50).IsRequired(true);
            builder.Property(a => a.WebAddress).HasMaxLength(100).IsRequired(true);
            builder.Property(a => a.Icon).HasMaxLength(100).IsRequired(false);
            builder.Property(a => a.Description).HasMaxLength(255).IsRequired(false);


            builder.Property(a => a.UserId).HasColumnType("int").IsRequired(true);

            builder.HasMany(u => u.projectImages).WithOne(a => a.Project).HasForeignKey(a => a.ProjectId);
            builder.HasOne(a => a.User).WithMany(u => u.Projects).HasForeignKey(a => a.UserId);

        }
    }
}
