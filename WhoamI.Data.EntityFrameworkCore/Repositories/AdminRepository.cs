using  WhoamI.Data.Contracts.Repositories;
using  WhoamI.Data.EntityFrameworkCore.Core.Repositories;
using WhoamI.Data.Entitys.Objects;

namespace  WhoamI.Data.EntityFrameworkCore.Repositories
{
    public class AdminRepository : EfCoreRepository<WhoamIDbContext, Admin, int>, IAdminRepository
    {
        public AdminRepository( WhoamIDbContext dbContext):base(dbContext)
        {
        }
    }
}
