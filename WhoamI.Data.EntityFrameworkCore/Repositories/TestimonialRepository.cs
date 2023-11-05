using  WhoamI.Data.Contracts.Repositories;
using  WhoamI.Data.EntityFrameworkCore.Core.Repositories;
using WhoamI.Data.Entitys.Objects;

namespace  WhoamI.Data.EntityFrameworkCore.Repositories
{
    public class TestimonialRepository : EfCoreRepository<WhoamIDbContext, Testimonial, int>, ITestimonialRepository
    {
        public TestimonialRepository( WhoamIDbContext dbContext):base(dbContext)
        {
        }
    }
}
