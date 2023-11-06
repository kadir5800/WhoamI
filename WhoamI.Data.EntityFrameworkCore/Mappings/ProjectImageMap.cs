using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WhoamI.Data.Entitys.Objects;

namespace WhoamI.Data.EntityFrameworkCore.Mappings
{
    public class ProjectImageMap : IEntityTypeConfiguration<ProjectImage>
    {
        public void Configure(EntityTypeBuilder<ProjectImage> builder)
        {

            builder.ToTable("ProjectImage");

            builder.HasKey(a => a.Id);

            builder.Property(u => u.CreationDate).HasColumnType("datetime2(7)").IsRequired(true);

            builder.Property(u => u.IsDeleted).HasColumnType("bit").IsRequired(true);

            builder.Property(a => a.Path).HasMaxLength(255).IsRequired(true);

            builder.Property(a => a.ProjectId).HasColumnType("int").IsRequired(true);
            builder.HasOne(a => a.Project).WithMany(u => u.projectImages).HasForeignKey(a => a.ProjectId);

        }
    }
}
