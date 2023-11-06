using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoamI.Business.Contracts.DTO.Education;
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
    public class EducationManager : BaseManager, IEducationManager
    {
        private readonly IEducationRepository _EducationRepository;
        private readonly WhoamIDbContext _dbContext;
        public EducationManager(IEducationRepository EducationRepository, WhoamIDbContext dbContext)
        {
            _EducationRepository = EducationRepository;
            _dbContext = dbContext;
        }

        public async Task<ClientResult> addEducation(addEducationRequest request)
        {

            if (request == null || String.IsNullOrEmpty(request.Degree) || String.IsNullOrEmpty(request.School))
                return Error(message: BusinesLocalization.FillRequiredFields, code: 402);


            var existingProvince = _EducationRepository.FirstOrDefault(t => t.School == request.School && t.EndDate == DateTime.Parse(request.EndDate.ToString()) && t.StartDate == DateTime.Parse(request.StartDate.ToString()) && t.UserId == request.UserId);


            if (existingProvince != null)
                return Error(message: BusinesLocalization.sameRecordAvailable, code: 402);

            await _EducationRepository.InsertAsync(new Education()
            {
                UserId = request.UserId,
                Degree = request.Degree,
                School = request.School,
                EndDate = DateTime.Parse(request.EndDate.ToString()),
                StartDate = DateTime.Parse(request.StartDate.ToString()),
                IsRunning = request.IsRunning,
                CreationDate = DateTime.Now,
                IsDeleted = false
            }, true);

            return Success(message: BusinesLocalization.InsertSuccess, code: 200);

        }

        public async Task<ClientResult> deleteEducation(getOneRequest request)
        {
            if (request == null || request.Id <= 0)
                return Error(message: BusinesLocalization.FillRequiredFields, code: 402);

            var existingProvince = _EducationRepository.FirstOrDefault(t => !t.IsDeleted && t.Id == request.Id);

            if (existingProvince == null)
                return Error(message: BusinesLocalization.NotFound, code: 404);

            existingProvince.IsDeleted = true;

            await _EducationRepository.UpdateAsync(existingProvince, true);
            return Success(message: BusinesLocalization.DeleteSuccess, code: 200);
        }

        public async Task<ClientResult<getAllEducationResponse>> getAllEducation(dataTableRequest request)
        {
            try
            {
                if (request == null)
                    return Error<getAllEducationResponse>(message: BusinesLocalization.FillRequiredFields, code: 402);

                int pageSize = request.Length != null ? Convert.ToInt32(request.Length) : 0;
                int skip = request.Start != null ? Convert.ToInt32(request.Start) : 0;

                int recordsTotal = 0;

                recordsTotal = _EducationRepository.Where(w => w.IsDeleted == false).Count();

                var takeA = request.Length == "-1" ? recordsTotal : pageSize;
                takeA = takeA == 0 ? 10 : takeA;

                var userSql = "";
                if (request.UserId > 0)
                {
                    userSql = $" AND [t0].[UserId]= {request.UserId}";
                }

                var sqlQuery = $@"SELECT [t0].* FROM [Education] AS [t0]  Where [t0].[IsDeleted] = 0 {userSql} AND [t0].[School] LIKE '%{request.SearchValue}%' ORDER BY [t0].[{request.SortColumn}] {request.SortColumnDir} OFFSET {skip} ROWS FETCH NEXT {takeA} ROWS ONLY";

                var query = await _dbContext.educations
                .FromSqlRaw(sqlQuery)
                .Select(u => new getOneEducationResponse()
                {
                    Id = u.Id,
                    Degree = u.Degree,
                    School = u.School,
                    UserId = u.UserId,
                    StartDate = DateTime.Parse(u.StartDate.ToString()).ToString(),
                    EndDate = DateTime.Parse(u.EndDate.ToString()).ToString(),
                    IsRunning = u.IsRunning,
                    CreationDate = DateTime.Parse(u.CreationDate.ToString()).ToString(),
                }).ToListAsync();

                var response = new getAllEducationResponse()
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
                return Success<getAllEducationResponse>(message: ex.Message, code: 500);
            }
        }

        public async Task<ClientResult<getOneEducationResponse>> getOneEducation(getOneRequest request)
        {
            if (request == null || request.Id <= 0)
                return Error<getOneEducationResponse>(message: BusinesLocalization.FillRequiredFields, code: 402);

            var existingBank = _EducationRepository.Where(t => !t.IsDeleted && t.Id == request.Id).Select(s => new getOneEducationResponse
            {
                Id = s.Id,
                Degree = s.Degree,
                School = s.School,
                UserId = s.UserId,
                StartDate = DateTime.Parse(s.StartDate.ToString()).ToString(),
                EndDate = DateTime.Parse(s.EndDate.ToString()).ToString(),
                IsRunning = s.IsRunning,
                CreationDate = DateTime.Parse(s.CreationDate.ToString()).ToString(),
            }).FirstOrDefault();

            if (existingBank == null)
                return Error<getOneEducationResponse>(message: BusinesLocalization.NotFound, code: 404);

            return Success<getOneEducationResponse>(message: BusinesLocalization.Success, code: 200, data: existingBank);
        }

        public async Task<ClientResult> updateEducation(updateEducationRequest request)
        {
            if (request == null || request.Id <= 0)
                return Error(message: BusinesLocalization.FillRequiredFields, code: 402);

            var existingEducation = _EducationRepository.FirstOrDefault(t => !t.IsDeleted && t.Id == request.Id);
            if (existingEducation == null)
                return Error(message: BusinesLocalization.NotFound, code: 404);

            existingEducation.Degree = request.Degree;
            existingEducation.School = request.School;
            existingEducation.StartDate = DateTime.Parse(request.StartDate.ToString());
            existingEducation.EndDate = DateTime.Parse(request.EndDate.ToString());
            existingEducation.IsRunning = request.IsRunning;

            await _EducationRepository.UpdateAsync(existingEducation, true);

            return Success(message: BusinesLocalization.UpdateSuccess, code: 200);
        }
    }
}
