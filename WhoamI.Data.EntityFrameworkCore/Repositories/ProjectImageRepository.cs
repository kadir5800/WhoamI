using  WhoamI.Data.Contracts.Repositories;
using  WhoamI.Data.EntityFrameworkCore.Core.Repositories;
using WhoamI.Data.Entitys.Objects;

namespace  WhoamI.Data.EntityFrameworkCore.Repositories
{
    public class ProjectImageRepository : EfCoreRepository<WhoamIDbContext, ProjectImage, int>, IProjectImageRepository
    {
        public ProjectImageRepository( WhoamIDbContext dbContext):base(dbContext)
        {
        }
    }
}
