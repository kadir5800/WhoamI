using  WhoamI.Data.Contracts.Repositories;
using  WhoamI.Data.EntityFrameworkCore.Core.Repositories;
using WhoamI.Data.Entitys.Objects;

namespace  WhoamI.Data.EntityFrameworkCore.Repositories
{
    public class EducationRepository : EfCoreRepository<WhoamIDbContext, Education, int>, IEducationRepository
    {
        public EducationRepository( WhoamIDbContext dbContext):base(dbContext)
        {
        }
    }
}
