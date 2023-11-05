using Microsoft.EntityFrameworkCore;
using WhoamI.Business.Contracts.DTO.Client;
using WhoamI.Business.Contracts.DTO.DataTable;
using WhoamI.Business.Contracts.DTO.Testimonial;
using WhoamI.Business.Contracts.IManager;
using WhoamI.Business.Localization;
using WhoamI.Data.Contracts.Repositories;
using WhoamI.Data.EntityFrameworkCore;
using WhoamI.Data.Entitys.Objects;

namespace WhoamI.Business.Managers
{
    public class TestimonialManager : BaseManager, ITestimonialManager
    {
        private readonly ITestimonialRepository _TestimonialRepository;
        private readonly WhoamIDbContext _dbContext;
        public TestimonialManager(ITestimonialRepository TestimonialRepository, WhoamIDbContext dbContext)
        {
            _TestimonialRepository = TestimonialRepository;
            _dbContext = dbContext;
        }

        public async Task<ClientResult> addTestimonial(addTestimonialRequest request)
        {

            if (request == null || String.IsNullOrEmpty(request.Name) || String.IsNullOrEmpty(request.Job) || String.IsNullOrEmpty(request.Surname))
                return Error(message: BusinesLocalization.FillRequiredFields, code: 402);


            var existingProvince = _TestimonialRepository.FirstOrDefault(t => t.Name == request.Name && t.Surname == request.Surname && t.UserId == request.UserId);


            if (existingProvince != null)
                return Error(message: BusinesLocalization.sameRecordAvailable, code: 402);

            await _TestimonialRepository.InsertAsync(new Testimonial()
            {
                UserId = request.UserId,
                Name = request.Name,
                Surname = request.Surname,
                Opinion = request.Opinion,
                Job = request.Job,
                CreationDate = DateTime.Now,
                IsDeleted = false
            }, true);

            return Success(message: BusinesLocalization.InsertSuccess, code: 200);

        }

        public async Task<ClientResult> deleteTestimonial(getOneRequest request)
        {
            if (request == null || request.Id <= 0)
                return Error(message: BusinesLocalization.FillRequiredFields, code: 402);

            var existingProvince = _TestimonialRepository.FirstOrDefault(t => !t.IsDeleted && t.Id == request.Id);

            if (existingProvince == null)
                return Error(message: BusinesLocalization.NotFound, code: 404);

            existingProvince.IsDeleted = true;

            await _TestimonialRepository.UpdateAsync(existingProvince, true);
            return Success(message: BusinesLocalization.DeleteSuccess, code: 200);
        }

        public async Task<ClientResult<getAllTestimonialResponse>> getAllTestimonial(dataTableRequest request)
        {
            try
            {
                if (request == null)
                    return Error<getAllTestimonialResponse>(message: BusinesLocalization.FillRequiredFields, code: 402);

                int pageSize = request.Length != null ? Convert.ToInt32(request.Length) : 0;
                int skip = request.Start != null ? Convert.ToInt32(request.Start) : 0;

                int recordsTotal = 0;

                recordsTotal = _TestimonialRepository.Where(w => w.IsDeleted == false).Count();

                var takeA = request.Length == "-1" ? recordsTotal : pageSize;
                takeA = takeA == 0 ? 10 : takeA;

                var userSql = "";
                if (request.UserId > 0)
                {
                    userSql = $" AND [t0].[UserId]= {request.UserId}";
                }

                var sqlQuery = $@"SELECT [t0].* FROM [Testimonial AS [t0] Where [t0].[IsDeleted] = 0 {userSql} AND [t0].[Name] LIKE '%{request.SearchValue}%' ORDER BY [t0].[{request.SortColumn}] {request.SortColumnDir} OFFSET {skip} ROWS FETCH NEXT {takeA} ROWS ONLY";

                var query = await _dbContext.testimonials
                .FromSqlRaw(sqlQuery)
                .Select(u => new getOneTestimonialResponse()
                {
                    Id = u.Id,
                    Name = u.Name,
                    UserId = u.UserId,
                    Surname = u.Surname,
                    Opinion = u.Opinion,
                    Job = u.Job,
                    CreationDate = DateTime.Parse(u.CreationDate.ToString()).ToString(),
                }).ToListAsync();

                var response = new getAllTestimonialResponse()
                {
                    draw = request.Draw,
                    recordsFiltered = recordsTotal,
                    recordsTotal = recordsTotal,
                    data = query
                };

                return Success(message: BusinesLocalization.Success, data: response);
            }
            catch (Exception ex)
            {
                return Success<getAllTestimonialResponse>(message: ex.Message, code: 500);
            }
        }

        public async Task<ClientResult<getOneTestimonialResponse>> getOneTestimonial(getOneRequest request)
        {
            if (request == null || request.Id <= 0)
                return Error<getOneTestimonialResponse>(message: BusinesLocalization.FillRequiredFields, code: 402);

            var existingBank = _TestimonialRepository.Where(t => !t.IsDeleted && t.Id == request.Id).Select(s => new getOneTestimonialResponse
            {
                Id = s.Id,
                Name = s.Name,
                UserId = s.UserId,
                Surname = s.Surname,
                Opinion = s.Opinion,
                Job = s.Job,
                CreationDate = DateTime.Parse(s.CreationDate.ToString()).ToString(),
            }).FirstOrDefault();

            if (existingBank == null)
                return Error<getOneTestimonialResponse>(message: BusinesLocalization.NotFound, code: 404);

            return Success<getOneTestimonialResponse>(message: BusinesLocalization.Success, code: 200, data: existingBank);
        }

        public async Task<ClientResult> updateTestimonial(updateTestimonialRequest request)
        {
            if (request == null || request.Id <= 0)
                return Error(message: BusinesLocalization.FillRequiredFields, code: 402);

            var existingTestimonial = _TestimonialRepository.FirstOrDefault(t => !t.IsDeleted && t.Id == request.Id);
            if (existingTestimonial == null)
                return Error(message: BusinesLocalization.NotFound, code: 404);

            existingTestimonial.Name = request.Name;
            existingTestimonial.Surname = request.Surname;
            existingTestimonial.Opinion = request.Opinion;
            existingTestimonial.Job = request.Job;
            existingTestimonial.UserId = request.UserId;

            await _TestimonialRepository.UpdateAsync(existingTestimonial, true);

            return Success(message: BusinesLocalization.UpdateSuccess, code: 200);
        }
    }
}
