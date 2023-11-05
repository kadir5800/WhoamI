using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WhoamI.Data.Entitys.Objects;

namespace WhoamI.Data.EntityConfigurations
{
    public class AbilityMap : IEntityTypeConfiguration<Ability>
    {
        public void Configure(EntityTypeBuilder<Ability> builder)
        {
            
             builder.ToTable("Abilities");

            builder.HasKey(a => a.Id);

            builder.Property(u => u.CreationDate).HasColumnType("datetime2(7)").IsRequired(true);

            builder.Property(u => u.IsDeleted).HasColumnType("bit").IsRequired(true);

            builder.Property(a => a.Degree).HasColumnType("int").IsRequired(true);

            builder.Property(a => a.Name).HasMaxLength(50).IsRequired(true);

            builder.Property(a => a.AbilityType).HasColumnType("int").IsRequired(true);

            builder.Property(a => a.UserId).HasColumnType("int").IsRequired(true);

            builder.HasOne(a => a.User).WithMany(u => u.Abilities).HasForeignKey(a => a.UserId);

        }
    }
}
