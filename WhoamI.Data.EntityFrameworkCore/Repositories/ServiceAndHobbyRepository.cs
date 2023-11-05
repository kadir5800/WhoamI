using  WhoamI.Data.Contracts.Repositories;
using  WhoamI.Data.EntityFrameworkCore.Core.Repositories;
using WhoamI.Data.Entitys.Objects;

namespace  WhoamI.Data.EntityFrameworkCore.Repositories
{
    public class ServiceAndHobbyRepository : EfCoreRepository<WhoamIDbContext, ServiceAndHobby, int>, IServiceAndHobbyRepository
    {
        public ServiceAndHobbyRepository( WhoamIDbContext dbContext):base(dbContext)
        {
        }
    }
}
