using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WhoamI.Data.Entitys.Objects;

namespace  WhoamI.Data.EntityFrameworkCore.Mappings
{
    public class UserMap: IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(q => q.Id);
            builder.Property(u => u.Name).HasMaxLength(50).IsRequired(true);    

            builder.Property(u => u.Surname).HasMaxLength(50).IsRequired(true);

            builder.Property(u => u.Email).HasMaxLength(50).IsRequired(true);

            builder.Property(u => u.CreationDate).HasColumnType("datetime2(7)").IsRequired(true);

            builder.Property(u => u.IsDeleted).HasColumnType("bit").IsRequired(true);

            builder.HasMany(u => u.UserContacts)
                .WithOne(uc => uc.User)
                .HasForeignKey(uc => uc.UserId);

            builder.HasMany(u => u.Experinces)
               .WithOne(e => e.User)
               .HasForeignKey(e => e.UserId);

            builder.HasMany(u => u.Articles)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId); 
            
            builder.HasMany(u => u.Educations)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);

            builder.HasMany(u => u.Experinces)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);

            builder.HasMany(u => u.Portfolios)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);

            builder.HasMany(u => u.Projects)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);

            builder.HasMany(u => u.ServiceAndHobbies)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);

            builder.HasMany(u => u.SocialMedias)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);

            builder.HasMany(u => u.Testimonials)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);

        }
    }
}
