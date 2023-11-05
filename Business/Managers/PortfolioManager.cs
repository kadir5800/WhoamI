using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoamI.Business.Contracts.DTO.Portfolio;
using WhoamI.Business.Contracts.DTO.Client;
using WhoamI.Business.Contracts.DTO.DataTable;
using WhoamI.Business.Contracts.IManager;
using WhoamI.Business.Localization;
using WhoamI.Data.Contracts.Repositories;
using WhoamI.Data.EntityFrameworkCore;
using WhoamI.Data.Entitys.Objects;
using Microsoft.EntityFrameworkCore;

namespace WhoamI.Business.Managers
{
    public class PortfolioManager : BaseManager, IPortfolioManager
    {
        private readonly IPortfolioRepository _PortfolioRepository;
        private readonly WhoamIDbContext _dbContext;
        public PortfolioManager(IPortfolioRepository PortfolioRepository, WhoamIDbContext dbContext)
        {
            _PortfolioRepository = PortfolioRepository;
            _dbContext = dbContext;
        }

        public async Task<ClientResult> addPortfolio(addPortfolioRequest request)
        {

            if (request == null || String.IsNullOrEmpty(request.Description) || String.IsNullOrEmpty(request.Name))
                return Error(message: BusinesLocalization.FillRequiredFields, code: 402);


            var existingProvince = _PortfolioRepository.FirstOrDefault(t => t.Name == request.Name && t.Description == request.Description && t.UserId == request.UserId);


            if (existingProvince != null)
                return Error(message: BusinesLocalization.sameRecordAvailable, code: 402);

            await _PortfolioRepository.InsertAsync(new Portfolio()
            {
                UserId = request.UserId,
                Name = request.Name,
                PortfolioType = request.PortfolioType,
                Description = request.Description,
                CreationDate = DateTime.Now,
                IsDeleted = false
            }, true);

            return Success(message: BusinesLocalization.InsertSuccess, code: 200);

        }

        public async Task<ClientResult> deletePortfolio(getOneRequest request)
        {
            if (request == null || request.Id <= 0)
                return Error(message: BusinesLocalization.FillRequiredFields, code: 402);

            var existingProvince = _PortfolioRepository.FirstOrDefault(t => !t.IsDeleted && t.Id == request.Id);

            if (existingProvince == null)
                return Error(message: BusinesLocalization.NotFound, code: 404);

            existingProvince.IsDeleted = true;

            await _PortfolioRepository.UpdateAsync(existingProvince, true);
            return Success(message: BusinesLocalization.DeleteSuccess, code: 200);
        }

        public async Task<ClientResult<getAllPortfolioResponse>> getAllPortfolio(dataTableRequest request)
        {
            try
            {
                if (request == null)
                    return Error<getAllPortfolioResponse>(message: BusinesLocalization.FillRequiredFields, code: 402);

                int pageSize = request.Length != null ? Convert.ToInt32(request.Length) : 0;
                int skip = request.Start != null ? Convert.ToInt32(request.Start) : 0;

                int recordsTotal = 0;

                recordsTotal = _PortfolioRepository.Where(w => w.IsDeleted == false).Count();

                var takeA = request.Length == "-1" ? recordsTotal : pageSize;
                takeA = takeA == 0 ? 10 : takeA;

                var userSql = "";
                if (request.UserId > 0)
                {
                    userSql = $" AND [t0].[UserId]= {request.UserId}";
                }

                var sqlQuery = $@"SELECT [t0].* FROM [Portfolio] AS [t0] Where [t0].[IsDeleted] = 0 {userSql} AND [t0].[Name] LIKE '%{request.SearchValue}%' ORDER BY [t0].[{request.SortColumn}] {request.SortColumnDir} OFFSET {skip} ROWS FETCH NEXT {takeA} ROWS ONLY";

                var query = await _dbContext.portfolios
                .FromSqlRaw(sqlQuery)
                .Select(u => new getOnePortfolioResponse()
                {
                    Id = u.Id,
                    Name = u.Name,
                    Description = u.Description,
                    UserId = u.UserId,
                    PortfolioType=u.PortfolioType,
                    CreationDate = DateTime.Parse(u.CreationDate.ToString()).ToString(),
                }).ToListAsync();

                var response = new getAllPortfolioResponse()
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
                return Success<getAllPortfolioResponse>(message: ex.Message, code: 500);
            }
        }

        public async Task<ClientResult<getOnePortfolioResponse>> getOnePortfolio(getOneRequest request)
        {
            if (request == null || request.Id <= 0)
                return Error<getOnePortfolioResponse>(message: BusinesLocalization.FillRequiredFields, code: 402);

            var existingBank = _PortfolioRepository.Where(t => !t.IsDeleted && t.Id == request.Id).Select(s => new getOnePortfolioResponse
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                UserId = s.UserId,
                PortfolioType = s.PortfolioType,
                CreationDate = DateTime.Parse(s.CreationDate.ToString()).ToString(),
            }).FirstOrDefault();

            if (existingBank == null)
                return Error<getOnePortfolioResponse>(message: BusinesLocalization.NotFound, code: 404);

            return Success<getOnePortfolioResponse>(message: BusinesLocalization.Success, code: 200, data: existingBank);
        }

        public async Task<ClientResult> updatePortfolio(updatePortfolioRequest request)
        {
            if (request == null || request.Id <= 0)
                return Error(message: BusinesLocalization.FillRequiredFields, code: 402);

            var existingPortfolio = _PortfolioRepository.FirstOrDefault(t => !t.IsDeleted && t.Id == request.Id);
            if (existingPortfolio == null)
                return Error(message: BusinesLocalization.NotFound, code: 404);

            existingPortfolio.Name = request.Name;
            existingPortfolio.PortfolioType = request.PortfolioType;
            existingPortfolio.Description = request.Description;
            existingPortfolio.UserId = request.UserId;

            await _PortfolioRepository.UpdateAsync(existingPortfolio, true);

            return Success(message: BusinesLocalization.UpdateSuccess, code: 200);
        }
    }
}
