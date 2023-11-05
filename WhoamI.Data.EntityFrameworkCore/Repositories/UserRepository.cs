using  WhoamI.Data.Contracts.Repositories;
using  WhoamI.Data.EntityFrameworkCore.Core.Repositories;
using WhoamI.Data.Entitys.Objects;

namespace  WhoamI.Data.EntityFrameworkCore.Repositories
{
    public class UserRepository : EfCoreRepository<WhoamIDbContext, User, int>, IUsersRepository
    {
        public UserRepository( WhoamIDbContext dbContext):base(dbContext)
        {
        }
    }
}
