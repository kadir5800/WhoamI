using  WhoamI.Data.Contracts.Repositories;
using  WhoamI.Data.EntityFrameworkCore.Core.Repositories;
using WhoamI.Data.Entitys.Objects;

namespace  WhoamI.Data.EntityFrameworkCore.Repositories
{
    public class UserContactRepository : EfCoreRepository<WhoamIDbContext, UserContact, int>, IUserContactRepository
    {
        public UserContactRepository( WhoamIDbContext dbContext):base(dbContext)
        {
        }
    }
}
