using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoamI.Business.Contracts.DTO.SocialMedia;
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
    public class SocialMediaManager : BaseManager, ISocialMediaManager
    {
        private readonly ISocialMediaRepository _SocialMediaRepository;
        private readonly WhoamIDbContext _dbContext;
        public SocialMediaManager(ISocialMediaRepository SocialMediaRepository, WhoamIDbContext dbContext)
        {
            _SocialMediaRepository = SocialMediaRepository;
            _dbContext = dbContext;
        }

        public async Task<ClientResult> addSocialMedia(addSocialMediaRequest request)
        {

            if (request == null || String.IsNullOrEmpty(request.Description) || String.IsNullOrEmpty(request.Name))
                return Error(message: BusinesLocalization.FillRequiredFields, code: 402);


            var existingProvince = _SocialMediaRepository.FirstOrDefault(t => t.Name == request.Name && t.Description == request.Description && t.UserId == request.UserId);


            if (existingProvince != null)
                return Error(message: BusinesLocalization.sameRecordAvailable, code: 402);

            await _SocialMediaRepository.InsertAsync(new SocialMedia()
            {
                UserId = request.UserId,
                Name = request.Name,
                Logo = request.Logo,
                Description = request.Description,
                CreationDate = DateTime.Now,
                IsDeleted = false
            }, true);

            return Success(message: BusinesLocalization.InsertSuccess, code: 200);

        }

        public async Task<ClientResult> deleteSocialMedia(getOneRequest request)
        {
            if (request == null || request.Id <= 0)
                return Error(message: BusinesLocalization.FillRequiredFields, code: 402);

            var existingProvince = _SocialMediaRepository.FirstOrDefault(t => !t.IsDeleted && t.Id == request.Id);

            if (existingProvince == null)
                return Error(message: BusinesLocalization.NotFound, code: 404);

            existingProvince.IsDeleted = true;

            await _SocialMediaRepository.UpdateAsync(existingProvince, true);
            return Success(message: BusinesLocalization.DeleteSuccess, code: 200);
        }

        public async Task<ClientResult<getAllSocialMediaResponse>> getAllSocialMedia(dataTableRequest request)
        {
            try
            {
                if (request == null)
                    return Error<getAllSocialMediaResponse>(message: BusinesLocalization.FillRequiredFields, code: 402);

                int pageSize = request.Length != null ? Convert.ToInt32(request.Length) : 0;
                int skip = request.Start != null ? Convert.ToInt32(request.Start) : 0;

                int recordsTotal = 0;

                recordsTotal = _SocialMediaRepository.Where(w => w.IsDeleted == false).Count();

                var takeA = request.Length == "-1" ? recordsTotal : pageSize;
                takeA = takeA == 0 ? 10 : takeA;

                var userSql = "";
                if (request.UserId > 0)
                {
                    userSql = $" AND [t0].[UserId]= {request.UserId}";
                }

                var sqlQuery = $@"SELECT [t0].* FROM [SocialMedia] AS [t0] Where [t0].[IsDeleted] = 0 {userSql} AND [t0].[Name] LIKE '%{request.SearchValue}%' ORDER BY [t0].[{request.SortColumn}] {request.SortColumnDir} OFFSET {skip} ROWS FETCH NEXT {takeA} ROWS ONLY";

                var query = await _dbContext.socialMedias
                .FromSqlRaw(sqlQuery)
                .Select(u => new getOneSocialMediaResponse()
                {
                    Id = u.Id,
                    Name = u.Name,
                    Description = u.Description,
                    UserId = u.UserId,
                    Logo = u.Logo,
                    CreationDate = DateTime.Parse(u.CreationDate.ToString()).ToString(),
                }).ToListAsync();

                var response = new getAllSocialMediaResponse()
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
                return Success<getAllSocialMediaResponse>(message: ex.Message, code: 500);
            }
        }

        public async Task<ClientResult<getOneSocialMediaResponse>> getOneSocialMedia(getOneRequest request)
        {
            if (request == null || request.Id <= 0)
                return Error<getOneSocialMediaResponse>(message: BusinesLocalization.FillRequiredFields, code: 402);

            var existingBank = _SocialMediaRepository.Where(t => !t.IsDeleted && t.Id == request.Id).Select(s => new getOneSocialMediaResponse
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                UserId = s.UserId,
                Logo = s.Logo,
                CreationDate = DateTime.Parse(s.CreationDate.ToString()).ToString(),
            }).FirstOrDefault();

            if (existingBank == null)
                return Error<getOneSocialMediaResponse>(message: BusinesLocalization.NotFound, code: 404);

            return Success<getOneSocialMediaResponse>(message: BusinesLocalization.Success, code: 200, data: existingBank);
        }

        public async Task<ClientResult> updateSocialMedia(updateSocialMediaRequest request)
        {
            if (request == null || request.Id <= 0)
                return Error(message: BusinesLocalization.FillRequiredFields, code: 402);

            var existingSocialMedia = _SocialMediaRepository.FirstOrDefault(t => !t.IsDeleted && t.Id == request.Id);
            if (existingSocialMedia == null)
                return Error(message: BusinesLocalization.NotFound, code: 404);

            existingSocialMedia.Name = request.Name;
            existingSocialMedia.Logo = request.Logo;
            existingSocialMedia.Description = request.Description;

            await _SocialMediaRepository.UpdateAsync(existingSocialMedia, true);

            return Success(message: BusinesLocalization.UpdateSuccess, code: 200);
        }
    }
}
