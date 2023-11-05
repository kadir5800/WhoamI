using  WhoamI.Data.Contracts.Repositories;
using  WhoamI.Data.EntityFrameworkCore.Core.Repositories;
using WhoamI.Data.Entitys.Objects;

namespace  WhoamI.Data.EntityFrameworkCore.Repositories
{
    public class ExperinceRepository : EfCoreRepository<WhoamIDbContext, Experince, int>, IExperinceRepository
    {
        public ExperinceRepository( WhoamIDbContext dbContext):base(dbContext)
        {
        }
    }
}
