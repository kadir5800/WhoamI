using  WhoamI.Data.Contracts.Repositories;
using  WhoamI.Data.EntityFrameworkCore.Core.Repositories;
using WhoamI.Data.Entitys.Objects;

namespace  WhoamI.Data.EntityFrameworkCore.Repositories
{
    public class PortfolioRepository : EfCoreRepository<WhoamIDbContext, Portfolio, int>, IPortfolioRepository
    {
        public PortfolioRepository( WhoamIDbContext dbContext):base(dbContext)
        {
        }
    }
}
