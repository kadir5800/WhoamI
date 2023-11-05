using Microsoft.EntityFrameworkCore;
using WhoamI.Business.Contracts.DTO.Ability;
using WhoamI.Business.Contracts.DTO.Client;
using WhoamI.Business.Contracts.DTO.DataTable;
using WhoamI.Business.Contracts.IManager;
using WhoamI.Business.Localization;
using WhoamI.Data.Contracts.Repositories;
using WhoamI.Data.EntityFrameworkCore;
using WhoamI.Data.Entitys.Objects;

namespace WhoamI.Business.Managers
{
    public class AbilityManager : BaseManager, IAbilityManager
    {
        private readonly IAbilityRepository _AbilitysRepository;
        private readonly WhoamIDbContext _dbContext;
        public AbilityManager(IAbilityRepository AbilitysRepository, WhoamIDbContext dbContext)
        {
            _AbilitysRepository = AbilitysRepository;
            _dbContext = dbContext;
        }

        public async Task<ClientResult> addAbility(addAbilityRequest request)
        {

            if (request == null || String.IsNullOrEmpty(request.Name) || request.Degree <= 0)
                return Error(message: BusinesLocalization.FillRequiredFields, code: 402);


            var existingProvince = _AbilitysRepository.FirstOrDefault(t => t.Name == request.Name && t.Degree == request.Degree && t.UserId == request.UserId);


            if (existingProvince != null)
                return Error(message: BusinesLocalization.sameRecordAvailable, code: 402);

            await _AbilitysRepository.InsertAsync(new Ability()
            {
                AbilityType = request.AbilityType,
                Name = request.Name,
                UserId = request.UserId,
                Degree = request.Degree,
                CreationDate = DateTime.Now,
                IsDeleted = false
            }, true);

            return Success(message: BusinesLocalization.InsertSuccess, code: 200);

        }

        public async Task<ClientResult> deleteAbility(getOneRequest request)
        {
            if (request == null || request.Id <= 0)
                return Error(message: BusinesLocalization.FillRequiredFields, code: 402);

            var existingProvince = _AbilitysRepository.FirstOrDefault(t => !t.IsDeleted && t.Id == request.Id);

            if (existingProvince == null)
                return Error(message: BusinesLocalization.NotFound, code: 404);

            existingProvince.IsDeleted = true;

            await _AbilitysRepository.UpdateAsync(existingProvince, true);
            return Success(message: BusinesLocalization.DeleteSuccess, code: 200);
        }

        public async Task<ClientResult<getAllAbilityResponse>> getAllAbility(dataTableRequest request)
        {
            try
            {
                if (request == null)
                    return Error<getAllAbilityResponse>(message: BusinesLocalization.FillRequiredFields, code: 402);

                int pageSize = request.Length != null ? Convert.ToInt32(request.Length) : 0;
                int skip = request.Start != null ? Convert.ToInt32(request.Start) : 0;

                int recordsTotal = 0;

                recordsTotal = _AbilitysRepository.Where(w => w.IsDeleted == false).Count();

                var takeA = request.Length == "-1" ? recordsTotal : pageSize;
                takeA = takeA == 0 ? 10 : takeA;
                var userSql = "";
                if (request.UserId>0)
                {
                    userSql = $" AND [t0].[UserId]= {request.UserId}";
                }

                var sqlQuery = $@"SELECT [t0].* FROM [Abilities] AS [t0]  Where [t0].[IsDeleted] = 0 {userSql} AND [t0].[Name] LIKE '%{request.SearchValue}%' ORDER BY [t0].[{request.SortColumn}] {request.SortColumnDir} OFFSET {skip} ROWS FETCH NEXT {takeA} ROWS ONLY";

                var query = await _dbContext.abilities
                .FromSqlRaw(sqlQuery)
                .Select(u => new getOneAbilityResponse()
                {
                    Id = u.Id,
                    Name = u.Name,
                    Degree = u.Degree,
                    UserId = u.UserId,
                    AbilityType = u.AbilityType,
                    CreationDate = DateTime.Parse(u.CreationDate.ToString()).ToString(),
                }).ToListAsync();

                var response = new getAllAbilityResponse()
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
                return Success<getAllAbilityResponse>(message: ex.Message, code: 500);
            }
        }

        public async Task<ClientResult<getOneAbilityResponse>> getOneAbility(getOneRequest request)
        {
            if (request == null || request.Id <= 0)
                return Error<getOneAbilityResponse>(message: BusinesLocalization.FillRequiredFields, code: 402);

            var existingBank = _AbilitysRepository.Where(t => !t.IsDeleted && t.Id == request.Id).Select(s => new getOneAbilityResponse
            {
                Id = s.Id,
                Name = s.Name,
                Degree = s.Degree,
                UserId = s.UserId,
                AbilityType = s.AbilityType,
                CreationDate = DateTime.Parse(s.CreationDate.ToString()).ToString(),
            }).FirstOrDefault();

            if (existingBank == null)
                return Error<getOneAbilityResponse>(message: BusinesLocalization.NotFound, code: 404);

            return Success<getOneAbilityResponse>(message: BusinesLocalization.Success, code: 200, data: existingBank);
        }

        public async Task<ClientResult> updateAbility(updateAbilityRequest request)
        {
            if (request == null || request.Id <= 0)
                return Error(message: BusinesLocalization.FillRequiredFields, code: 402);

            var existingAbility = _AbilitysRepository.FirstOrDefault(t => !t.IsDeleted && t.Id == request.Id);
            if (existingAbility == null)
                return Error(message: BusinesLocalization.NotFound, code: 404);

            existingAbility.Id = request.Id;
            existingAbility.Name = request.Name;
            existingAbility.Degree = request.Degree;
            existingAbility.AbilityType = request.AbilityType;

            await _AbilitysRepository.UpdateAsync(existingAbility, true);

            return Success(message: BusinesLocalization.UpdateSuccess, code: 200);
        }
    }
}
