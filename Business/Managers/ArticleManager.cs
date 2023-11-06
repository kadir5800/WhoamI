using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoamI.Business.Contracts.DTO.Client;
using WhoamI.Business.Contracts.DTO.DataTable;
using WhoamI.Business.Contracts.DTO.Article;
using WhoamI.Business.Contracts.IManager;
using WhoamI.Business.Localization;
using WhoamI.Data.Contracts.Repositories;
using WhoamI.Data.EntityFrameworkCore;
using WhoamI.Data.Entitys.Objects;
using Microsoft.EntityFrameworkCore;

namespace WhoamI.Business.Managers
{
    public class ArticleManager : BaseManager, IArticleManager
    {
        private readonly IArticleRepository _ArticleRepository;
        private readonly WhoamIDbContext _dbContext;
        private readonly PhotoManager _photoManager;
        public ArticleManager(IArticleRepository ArticleRepository, WhoamIDbContext dbContext, PhotoManager photoManager)
        {
            _ArticleRepository = ArticleRepository;
            _dbContext = dbContext;
            _photoManager = photoManager;
        }

        public async Task<ClientResult> addArticle(addArticleRequest request)
        {

            if (request == null || String.IsNullOrEmpty(request.Description) || String.IsNullOrEmpty(request.Name))
                return Error(message: BusinesLocalization.FillRequiredFields, code: 402);


            var existingProvince = _ArticleRepository.FirstOrDefault(t => t.Name == request.Name && t.Description == request.Description && t.UserId == request.UserId);


            if (existingProvince != null)
                return Error(message: BusinesLocalization.sameRecordAvailable, code: 402);

            var image = "";

            var path = _photoManager.UploadPhoto(request.file, "ArticleImages");
            if (path != null && !String.IsNullOrEmpty(path))
            {
                image = path;
            }
            else return Error(message: "Dosya Yükleme Başarısız", code: 402);


            await _ArticleRepository.InsertAsync(new Article()
            {
                UserId = request.UserId,
                Name = request.Name,
                Image= image,
                Description = request.Description,
                CreationDate = DateTime.Now,
                IsDeleted = false
            }, true);

            return Success(message: BusinesLocalization.InsertSuccess, code: 200);

        }

        public async Task<ClientResult> deleteArticle(getOneRequest request)
        {
            if (request == null || request.Id <= 0)
                return Error(message: BusinesLocalization.FillRequiredFields, code: 402);

            var existingProvince = _ArticleRepository.FirstOrDefault(t => !t.IsDeleted && t.Id == request.Id);

            if (existingProvince == null)
                return Error(message: BusinesLocalization.NotFound, code: 404);

            existingProvince.IsDeleted = true;
            _photoManager.DeletePhoto(existingProvince.Image);

            await _ArticleRepository.UpdateAsync(existingProvince, true);
            return Success(message: BusinesLocalization.DeleteSuccess, code: 200);
        }

        public async Task<ClientResult<getAllArticleResponse>> getAllArticle(dataTableRequest request)
        {
            try
            {
                if (request == null)
                    return Error<getAllArticleResponse>(message: BusinesLocalization.FillRequiredFields, code: 402);

                int pageSize = request.Length != null ? Convert.ToInt32(request.Length) : 0;
                int skip = request.Start != null ? Convert.ToInt32(request.Start) : 0;

                int recordsTotal = 0;

                recordsTotal = _ArticleRepository.Where(w => w.IsDeleted == false).Count();

                var takeA = request.Length == "-1" ? recordsTotal : pageSize;
                takeA = takeA == 0 ? 10 : takeA;

                var userSql = "";
                if (request.UserId > 0)
                {
                    userSql = $" AND [t0].[UserId]= {request.UserId}";
                }

                var sqlQuery = $@"SELECT [t0].* FROM [Article] AS [t0]  Where [t0].[IsDeleted] = 0 {userSql} AND [t0].[Name] LIKE '%{request.SearchValue}%' ORDER BY [t0].[{request.SortColumn}] {request.SortColumnDir} OFFSET {skip} ROWS FETCH NEXT {takeA} ROWS ONLY";

                var query = await _dbContext.articles
                .FromSqlRaw(sqlQuery)
                .Select(u => new getOneArticleResponse()
                {
                    Id = u.Id,
                    Name = u.Name,
                    Description = u.Description,
                    UserId = u.UserId,
                    Image = u.Image,
                    CreationDate = DateTime.Parse(u.CreationDate.ToString()).ToString(),
                }).ToListAsync();

                var response = new getAllArticleResponse()
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
                return Success<getAllArticleResponse>(message: ex.Message, code: 500);
            }
        }

        public async Task<ClientResult<getOneArticleResponse>> getOneArticle(getOneRequest request)
        {
            if (request == null || request.Id <= 0)
                return Error<getOneArticleResponse>(message: BusinesLocalization.FillRequiredFields, code: 402);

            var existingBank = _ArticleRepository.Where(t => !t.IsDeleted && t.Id == request.Id).Select(s => new getOneArticleResponse
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                UserId = s.UserId,
                Image = s.Image,
                CreationDate = DateTime.Parse(s.CreationDate.ToString()).ToString(),
            }).FirstOrDefault();

            if (existingBank == null)
                return Error<getOneArticleResponse>(message: BusinesLocalization.NotFound, code: 404);

            return Success<getOneArticleResponse>(message: BusinesLocalization.Success, code: 200, data: existingBank);
        }

        public async Task<ClientResult> updateArticle(updateArticleRequest request)
        {
            if (request == null || request.Id <= 0)
                return Error(message: BusinesLocalization.FillRequiredFields, code: 402);

            var existingArticle = _ArticleRepository.FirstOrDefault(t => !t.IsDeleted && t.Id == request.Id);
            if (existingArticle == null)
                return Error(message: BusinesLocalization.NotFound, code: 404);

            var image = existingArticle.Image;

            if (request.file!=null)
            {
                var path = _photoManager.UploadPhoto(request.file, "ArticleImages");
                if (path != null && !String.IsNullOrEmpty(path))
                {
                    image = path;
                }
                else return Error(message: "Dosya Yükleme Başarısız", code: 402);
            }
           
            existingArticle.Name = request.Name;
            existingArticle.Image = image;
            existingArticle.Description = request.Description;

            await _ArticleRepository.UpdateAsync(existingArticle, true);

            return Success(message: BusinesLocalization.UpdateSuccess, code: 200);
        }
    }
}
