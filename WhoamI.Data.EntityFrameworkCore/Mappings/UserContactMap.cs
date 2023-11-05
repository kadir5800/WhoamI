using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WhoamI.Data.Entitys.Objects;

namespace WhoamI.Data.EntityFrameworkCore.Mappings
{
    public class UserContactMap : IEntityTypeConfiguration<UserContact>
    {
        public void Configure(EntityTypeBuilder<UserContact> builder)
        {

            builder.ToTable("UserContact");

            builder.HasKey(a => a.Id);

            builder.Property(u => u.CreationDate).HasColumnType("datetime2(7)").IsRequired(true);

            builder.Property(u => u.IsDeleted).HasColumnType("bit").IsRequired(true);


            builder.Property(a => a.Address).HasMaxLength(255).IsRequired(false);
            builder.Property(a => a.City).HasMaxLength(100).IsRequired(false);
            builder.Property(a => a.Region).HasMaxLength(100).IsRequired(false);
            builder.Property(a => a.PostalCode).HasMaxLength(50).IsRequired(false);
            builder.Property(a => a.Phone).HasMaxLength(50).IsRequired(false);
            builder.Property(a => a.Country).HasMaxLength(255).IsRequired(false);
            builder.Property(a => a.AboutMe).HasMaxLength(255).IsRequired(false);


            builder.Property(a => a.UserId).HasColumnType("int").IsRequired(true);

            builder.HasOne(a => a.User).WithMany(u => u.UserContacts).HasForeignKey(a => a.UserId);

        }
    }
}
