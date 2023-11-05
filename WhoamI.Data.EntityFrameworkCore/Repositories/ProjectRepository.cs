using  WhoamI.Data.Contracts.Repositories;
using  WhoamI.Data.EntityFrameworkCore.Core.Repositories;
using WhoamI.Data.Entitys.Objects;

namespace  WhoamI.Data.EntityFrameworkCore.Repositories
{
    public class ProjectRepository : EfCoreRepository<WhoamIDbContext, Project, int>, IProjectRepository
    {
        public ProjectRepository( WhoamIDbContext dbContext):base(dbContext)
        {
        }
    }
}
