using Microsoft.EntityFrameworkCore;
using WhoamI.Business.Contracts.DTO.Client;
using WhoamI.Business.Contracts.DTO.DataTable;
using WhoamI.Business.Contracts.DTO.ServiceAndHobby;
using WhoamI.Business.Contracts.IManager;
using WhoamI.Business.Localization;
using WhoamI.Data.Contracts.Repositories;
using WhoamI.Data.EntityFrameworkCore;
using WhoamI.Data.Entitys.Objects;

namespace WhoamI.Business.Managers
{
    public class ServiceAndHobbyManager : BaseManager, IServiceAndHobbyManager
    {
        private readonly IServiceAndHobbyRepository _ServiceAndHobbyRepository;
        private readonly WhoamIDbContext _dbContext;
        public ServiceAndHobbyManager(IServiceAndHobbyRepository ServiceAndHobbyRepository, WhoamIDbContext dbContext)
        {
            _ServiceAndHobbyRepository = ServiceAndHobbyRepository;
            _dbContext = dbContext;
        }

        public async Task<ClientResult> addServiceAndHobby(addServiceAndHobbyRequest request)
        {

            if (request == null || request.UserId <= 0 || String.IsNullOrEmpty(request.Name))
                return Error(message: BusinesLocalization.FillRequiredFields, code: 402);


            var existingProvince = _ServiceAndHobbyRepository.FirstOrDefault(t => t.Name == request.Name && t.UserId == request.UserId);


            if (existingProvince != null)
                return Error(message: BusinesLocalization.sameRecordAvailable, code: 402);

            await _ServiceAndHobbyRepository.InsertAsync(new ServiceAndHobby()
            {
                UserId = request.UserId,
                Name = request.Name,
                IsService = request.IsService,
                CreationDate = DateTime.Now,
                IsDeleted = false
            }, true);

            return Success(message: BusinesLocalization.InsertSuccess, code: 200);

        }

        public async Task<ClientResult> deleteServiceAndHobby(getOneRequest request)
        {
            if (request == null || request.Id <= 0)
                return Error(message: BusinesLocalization.FillRequiredFields, code: 402);

            var existingProvince = _ServiceAndHobbyRepository.FirstOrDefault(t => !t.IsDeleted && t.Id == request.Id);

            if (existingProvince == null)
                return Error(message: BusinesLocalization.NotFound, code: 404);

            existingProvince.IsDeleted = true;

            await _ServiceAndHobbyRepository.UpdateAsync(existingProvince, true);
            return Success(message: BusinesLocalization.DeleteSuccess, code: 200);
        }

        public async Task<ClientResult<getAllServiceAndHobbyResponse>> getAllServiceAndHobby(dataTableRequest request)
        {
            try
            {
                if (request == null)
                    return Error<getAllServiceAndHobbyResponse>(message: BusinesLocalization.FillRequiredFields, code: 402);

                int pageSize = request.Length != null ? Convert.ToInt32(request.Length) : 0;
                int skip = request.Start != null ? Convert.ToInt32(request.Start) : 0;

                int recordsTotal = 0;

                recordsTotal = _ServiceAndHobbyRepository.Where(w => w.IsDeleted == false).Count();

                var takeA = request.Length == "-1" ? recordsTotal : pageSize;
                takeA = takeA == 0 ? 10 : takeA;
                
                var userSql = "";
                if (request.UserId > 0)
                {
                    userSql = $" AND [t0].[UserId]= {request.UserId}";
                }

                var sqlQuery = $@"SELECT [t0].* FROM [ ServiceAndHobby] AS [t0] Where [t0].[IsDeleted] = 0 {userSql} AND [t0].[Name] LIKE '%{request.SearchValue}%' ORDER BY [t0].[{request.SortColumn}] {request.SortColumnDir} OFFSET {skip} ROWS FETCH NEXT {takeA} ROWS ONLY";

                var query = await _dbContext.serviceAndHobbies
                .FromSqlRaw(sqlQuery)
                .Select(u => new getOneServiceAndHobbyResponse()
                {
                    Id = u.Id,
                    Name = u.Name,
                    IsService = u.IsService,
                    UserId = u.UserId,
                    CreationDate = DateTime.Parse(u.CreationDate.ToString()).ToString(),
                }).ToListAsync();

                var response = new getAllServiceAndHobbyResponse()
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
                return Success<getAllServiceAndHobbyResponse>(message: ex.Message, code: 500);
            }
        }

        public async Task<ClientResult<getOneServiceAndHobbyResponse>> getOneServiceAndHobby(getOneRequest request)
        {
            if (request == null || request.Id <= 0)
                return Error<getOneServiceAndHobbyResponse>(message: BusinesLocalization.FillRequiredFields, code: 402);

            var existingBank = _ServiceAndHobbyRepository.Where(t => !t.IsDeleted && t.Id == request.Id).Select(s => new getOneServiceAndHobbyResponse
            {
                Id = s.Id,
                Name = s.Name,
                IsService = s.IsService,
                UserId = s.UserId,
                CreationDate = DateTime.Parse(s.CreationDate.ToString()).ToString(),
            }).FirstOrDefault();

            if (existingBank == null)
                return Error<getOneServiceAndHobbyResponse>(message: BusinesLocalization.NotFound, code: 404);

            return Success<getOneServiceAndHobbyResponse>(message: BusinesLocalization.Success, code: 200, data: existingBank);
        }

        public async Task<ClientResult> updateServiceAndHobby(updateServiceAndHobbyRequest request)
        {
            if (request == null || request.Id <= 0)
                return Error(message: BusinesLocalization.FillRequiredFields, code: 402);

            var existingServiceAndHobby = _ServiceAndHobbyRepository.FirstOrDefault(t => !t.IsDeleted && t.Id == request.Id);
            if (existingServiceAndHobby == null)
                return Error(message: BusinesLocalization.NotFound, code: 404);

            existingServiceAndHobby.Name = request.Name;
            existingServiceAndHobby.IsService = request.IsService;

            await _ServiceAndHobbyRepository.UpdateAsync(existingServiceAndHobby, true);

            return Success(message: BusinesLocalization.UpdateSuccess, code: 200);
        }
    }
}
