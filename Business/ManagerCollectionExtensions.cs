using Microsoft.Extensions.DependencyInjection;
using WhoamI.Business.Contracts.IManager;
using WhoamI.Business.Managers;

namespace WhoamI.Business
{
    public static class ManagerCollectionExtensions
    {
        public static IServiceCollection AddWhoamIDataWithBusinessCollection(this IServiceCollection services)
        {
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IAbilityManager, AbilityManager>();
            services.AddScoped<IAuthenticationManager, AuthenticationManager>();
            services.AddScoped<IArticleManager, ArticleManager>();
            services.AddScoped<IEducationManager, EducationManager>();
            services.AddScoped<IExperinceManager, ExperinceManager>();
            services.AddScoped<IPortfolioManager, PortfolioManager>();
            services.AddScoped<IProjectImageManager, ProjectImageManager>();
            services.AddScoped<IProjectManager, ProjectManager>();
            services.AddScoped<IServiceAndHobbyManager, ServiceAndHobbyManager>();
            services.AddScoped<ISocialMediaManager, SocialMediaManager>();
            services.AddScoped<ITestimonialManager, TestimonialManager>();
            services.AddScoped<IUserContactManager, UserContactManager>();
            services.AddScoped<PhotoManager>();

            return services;
        }
    }
}
