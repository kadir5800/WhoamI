using Microsoft.Extensions.DependencyInjection;
using WhoamI.Data.Contracts.Repositories;
using WhoamI.Data.EntityFrameworkCore.Repositories;

namespace WhoamI.Data.EntityFrameworkCore
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWhoamIDataWithEntityFrameworkCollection(this IServiceCollection services)
        {
            services.AddScoped<IUsersRepository, UserRepository>();
            services.AddScoped<IAbilityRepository, AbilityRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<IEducationRepository, EducationRepository>();
            services.AddScoped<IExperinceRepository, ExperinceRepository>();
            services.AddScoped<IPortfolioRepository, PortfolioRepository>();
            services.AddScoped<IProjectImageRepository, ProjectImageRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IServiceAndHobbyRepository, ServiceAndHobbyRepository>();
            services.AddScoped<ISocialMediaRepository, SocialMediaRepository>();
            services.AddScoped<ITestimonialRepository, TestimonialRepository>();
            services.AddScoped<IUserContactRepository, UserContactRepository>();

            return services;
        }
    }
}

