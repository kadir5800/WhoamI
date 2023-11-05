using  WhoamI.Data.Contracts.Repositories;
using  WhoamI.Data.EntityFrameworkCore.Core.Repositories;
using WhoamI.Data.Entitys.Objects;

namespace  WhoamI.Data.EntityFrameworkCore.Repositories
{
    public class AbilityRepository : EfCoreRepository<WhoamIDbContext, Ability, int>, IAbilityRepository
    {
        public AbilityRepository( WhoamIDbContext dbContext):base(dbContext)
        {
        }
    }
}
