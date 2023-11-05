using  WhoamI.Data.Contracts.Repositories;
using  WhoamI.Data.EntityFrameworkCore.Core.Repositories;
using WhoamI.Data.Entitys.Objects;

namespace  WhoamI.Data.EntityFrameworkCore.Repositories
{
    public class SocialMediaRepository : EfCoreRepository<WhoamIDbContext, SocialMedia, int>, ISocialMediaRepository
    {
        public SocialMediaRepository( WhoamIDbContext dbContext):base(dbContext)
        {
        }
    }
}
