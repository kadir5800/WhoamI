using Microsoft.EntityFrameworkCore;
using WhoamI.Business.Contracts.DTO.Client;
using WhoamI.Business.Contracts.DTO.DataTable;
using WhoamI.Business.Contracts.DTO.Experince;
using WhoamI.Business.Contracts.IManager;
using WhoamI.Business.Localization;
using WhoamI.Data.Contracts.Repositories;
using WhoamI.Data.EntityFrameworkCore;
using WhoamI.Data.Entitys.Objects;

namespace WhoamI.Business.Managers
{
    public class ExperinceManager : BaseManager, IExperinceManager
    {
        private readonly IExperinceRepository _ExperinceRepository;
        private readonly WhoamIDbContext _dbContext;
        public ExperinceManager(IExperinceRepository ExperinceRepository, WhoamIDbContext dbContext)
        {
            _ExperinceRepository = ExperinceRepository;
            _dbContext = dbContext;
        }

        public async Task<ClientResult> addExperince(addExperinceRequest request)
        {

            if (request == null || String.IsNullOrEmpty(request.job) || String.IsNullOrEmpty(request.Company) || request.StartDate <= DateTime.MinValue)
                return Error(message: BusinesLocalization.FillRequiredFields, code: 402);

            var existingProvince = _ExperinceRepository.FirstOrDefault(t => t.job == request.job && t.Company == request.Company && t.UserId == request.UserId && t.StartDate == request.StartDate);

            if (existingProvince != null)
                return Error(message: BusinesLocalization.sameRecordAvailable, code: 402);

            await _ExperinceRepository.InsertAsync(new Experince()
            {
                UserId = request.UserId,
                job = request.job,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                IsRunning = request.IsRunning,
                Company = request.Company,
                CreationDate = DateTime.Now,
                IsDeleted = false
            }, true);

            return Success(message: BusinesLocalization.InsertSuccess, code: 200);

        }

        public async Task<ClientResult> deleteExperince(getOneRequest request)
        {
            if (request == null || request.Id <= 0)
                return Error(message: BusinesLocalization.FillRequiredFields, code: 402);

            var existingProvince = _ExperinceRepository.FirstOrDefault(t => !t.IsDeleted && t.Id == request.Id);

            if (existingProvince == null)
                return Error(message: BusinesLocalization.NotFound, code: 404);

            existingProvince.IsDeleted = true;

            await _ExperinceRepository.UpdateAsync(existingProvince, true);
            return Success(message: BusinesLocalization.DeleteSuccess, code: 200);
        }

        public async Task<ClientResult<getAllExperinceResponse>> getAllExperince(dataTableRequest request)
        {
            try
            {
                if (request == null)
                    return Error<getAllExperinceResponse>(message: BusinesLocalization.FillRequiredFields, code: 402);

                int pageSize = request.Length != null ? Convert.ToInt32(request.Length) : 0;
                int skip = request.Start != null ? Convert.ToInt32(request.Start) : 0;

                int recordsTotal = 0;

                recordsTotal = _ExperinceRepository.Where(w => w.IsDeleted == false).Count();

                var takeA = request.Length == "-1" ? recordsTotal : pageSize;
                takeA = takeA == 0 ? 10 : takeA;

                var sqlQuery = $@"SELECT [t0].* FROM Experince AS [t0] LIKE '%{request.SearchValue}%' ORDER BY [t0].[{request.SortColumn}] {request.SortColumnDir} OFFSET {skip} ROWS FETCH NEXT {takeA} ROWS ONLY";

                var query = await _dbContext.experinces
                .FromSqlRaw(sqlQuery)
                .Select(u => new getOneExperinceResponse()
                {
                    Id = u.Id,
                    job = u.job,
                    StartDate = u.StartDate,
                    EndDate = u.EndDate,
                    IsRunning = u.IsRunning,
                    Company = u.Company,
                    UserId=u.UserId,
                    CreationDate = DateTime.Parse(u.CreationDate.ToString()),
                }).ToListAsync();

                var response = new getAllExperinceResponse()
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
                return Success<getAllExperinceResponse>(message: ex.Message, code: 500);
            }
        }

        public async Task<ClientResult<getOneExperinceResponse>> getOneExperince(getOneRequest request)
        {
            if (request == null || request.Id <= 0)
                return Error<getOneExperinceResponse>(message: BusinesLocalization.FillRequiredFields, code: 402);

            var existingBank = _ExperinceRepository.Where(t => !t.IsDeleted && t.Id == request.Id).Select(s => new getOneExperinceResponse
            {
                Id = s.Id,
                job = s.job,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                IsRunning = s.IsRunning,
                Company = s.Company,
                UserId = s.UserId,
                CreationDate = s.CreationDate,
            }).FirstOrDefault();

            if (existingBank == null)
                return Error<getOneExperinceResponse>(message: BusinesLocalization.NotFound, code: 404);

            return Success<getOneExperinceResponse>(message: BusinesLocalization.Success, code: 200, data: existingBank);
        }

        public async Task<ClientResult> updateExperince(updateExperinceRequest request)
        {
            if (request == null || request.Id <= 0)
                return Error(message: BusinesLocalization.FillRequiredFields, code: 402);

            var existingExperince = _ExperinceRepository.FirstOrDefault(t => !t.IsDeleted && t.Id == request.Id);
            if (existingExperince == null)
                return Error(message: BusinesLocalization.NotFound, code: 404);

            existingExperince.StartDate = request.StartDate;
            existingExperince.job = request.job;
            existingExperince.EndDate = request.EndDate;
            existingExperince.IsRunning = request.IsRunning;
            existingExperince.Company = request.Company;
            existingExperince.UserId = request.UserId;

            await _ExperinceRepository.UpdateAsync(existingExperince, true);

            return Success(message: BusinesLocalization.UpdateSuccess, code: 200);
        }
    }
}
