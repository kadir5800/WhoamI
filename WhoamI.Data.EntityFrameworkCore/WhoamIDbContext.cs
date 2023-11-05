using Microsoft.EntityFrameworkCore;
using WhoamI.Data.EntityConfigurations;
using  WhoamI.Data.EntityFrameworkCore.Mappings;
using WhoamI.Data.Entitys.Objects;

namespace  WhoamI.Data.EntityFrameworkCore
{
    public class WhoamIDbContext : DbContext
    {
        public WhoamIDbContext(DbContextOptions<WhoamIDbContext> options) : base(options)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new AbilityMap());
            modelBuilder.ApplyConfiguration(new AdminMap());
            modelBuilder.ApplyConfiguration(new ArticleMap());
            modelBuilder.ApplyConfiguration(new EducationMap());
            modelBuilder.ApplyConfiguration(new ExperinceMap());
            modelBuilder.ApplyConfiguration(new PortfolioMap());
            modelBuilder.ApplyConfiguration(new ProjectImageMap());
            modelBuilder.ApplyConfiguration(new ProjectMap());
            modelBuilder.ApplyConfiguration(new ServiceAndHobbyMap());
            modelBuilder.ApplyConfiguration(new SocialMediaMap());
            modelBuilder.ApplyConfiguration(new TestimonialMap());
            modelBuilder.ApplyConfiguration(new UserContactMap());


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> users { get; set; }
        public DbSet<Ability> abilities { get; set; }
        public DbSet<Admin> admins { get; set; }
        public DbSet<Article> articles { get; set; }
        public DbSet<Education> educations { get; set; }
        public DbSet<Experince> experinces { get; set; }
        public DbSet<Portfolio> portfolios { get; set; }
        public DbSet<Project> projects { get; set; }
        public DbSet<ProjectImage> projectImages { get; set; }
        public DbSet<ServiceAndHobby> serviceAndHobbies { get; set; }
        public DbSet<SocialMedia> socialMedias { get; set; }
        public DbSet<Testimonial> testimonials { get; set; }
        public DbSet<UserContact> userContacts { get; set; }
     
    }
}
