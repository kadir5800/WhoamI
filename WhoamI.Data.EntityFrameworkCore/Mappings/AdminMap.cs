using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WhoamI.Data.Entitys.Objects;

namespace  WhoamI.Data.EntityFrameworkCore.Mappings
{
    public class AdminMap : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.ToTable("Admin");
          

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Username).HasMaxLength(50).IsRequired(true);
            builder.Property(a => a.Password).HasMaxLength(255).IsRequired(true);

        }
    }
}
