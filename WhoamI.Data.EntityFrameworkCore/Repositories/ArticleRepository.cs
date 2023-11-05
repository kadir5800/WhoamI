using  WhoamI.Data.Contracts.Repositories;
using  WhoamI.Data.EntityFrameworkCore.Core.Repositories;
using WhoamI.Data.Entitys.Objects;

namespace  WhoamI.Data.EntityFrameworkCore.Repositories
{
    public class ArticleRepository : EfCoreRepository<WhoamIDbContext, Article, int>, IArticleRepository
    {
        public ArticleRepository( WhoamIDbContext dbContext):base(dbContext)
        {
        }
    }
}
