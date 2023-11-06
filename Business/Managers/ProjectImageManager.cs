using Microsoft.EntityFrameworkCore;
using WhoamI.Business.Contracts.DTO.Client;
using WhoamI.Business.Contracts.DTO.DataTable;
using WhoamI.Business.Contracts.DTO.ProjectImage;
using WhoamI.Business.Contracts.IManager;
using WhoamI.Business.Localization;
using WhoamI.Data.Contracts.Repositories;
using WhoamI.Data.EntityFrameworkCore;
using WhoamI.Data.Entitys.Objects;

namespace WhoamI.Business.Managers
{
    public class ProjectImageManager : BaseManager, IProjectImageManager
    {
        private readonly IProjectImageRepository _ProjectImageRepository;
        private readonly WhoamIDbContext _dbContext;
        private readonly PhotoManager _photoManager;
        public ProjectImageManager(IProjectImageRepository ProjectImageRepository, WhoamIDbContext dbContext, PhotoManager photoManager)
        {
            _ProjectImageRepository = ProjectImageRepository;
            _dbContext = dbContext;
            _photoManager = photoManager;
        }

        public async Task<ClientResult> addProjectImage(addProjectImageRequest request)
        {

            if (request == null || request.ProjectId <= 0 || request.file == null)
                return Error(message: BusinesLocalization.FillRequiredFields, code: 402);

            var image = "";

            var path = _photoManager.UploadPhoto(request.file, "ProjectImages");
            if (path != null && !String.IsNullOrEmpty(path))
            {
                image = path;
            }
            else return Error(message: "Dosya Yükleme Başarısız", code: 402);


            await _ProjectImageRepository.InsertAsync(new ProjectImage()
            {
                ProjectId = request.ProjectId,
                Path = image,
                CreationDate = DateTime.Now,
                IsDeleted = false
            }, true);

            return Success(message: BusinesLocalization.InsertSuccess, code: 200);

        }

        public async Task<ClientResult> deleteProjectImage(getOneRequest request)
        {
            if (request == null || request.Id <= 0)
                return Error(message: BusinesLocalization.FillRequiredFields, code: 402);

            var existing = _ProjectImageRepository.FirstOrDefault(t => !t.IsDeleted && t.Id == request.Id);

            if (existing == null)
                return Error(message: BusinesLocalization.NotFound, code: 404);

            existing.IsDeleted = true;
            _photoManager.DeletePhoto(existing.Path);

            await _ProjectImageRepository.UpdateAsync(existing, true);
            return Success(message: BusinesLocalization.DeleteSuccess, code: 200);
        }

        public async Task<ClientResult<getAllProjectImageResponse>> getAllProjectImage(dataTableRequest request)
        {
            try
            {
                if (request == null)
                    return Error<getAllProjectImageResponse>(message: BusinesLocalization.FillRequiredFields, code: 402);

                int pageSize = request.Length != null ? Convert.ToInt32(request.Length) : 0;
                int skip = request.Start != null ? Convert.ToInt32(request.Start) : 0;

                int recordsTotal = 0;

                recordsTotal = _ProjectImageRepository.Where(w => w.IsDeleted == false).Count();

                var takeA = request.Length == "-1" ? recordsTotal : pageSize;
                takeA = takeA == 0 ? 10 : takeA;

                var userSql = "";
                if (request.UserId > 0)
                {
                    userSql = $" AND [t0].[ProjectId]= {request.UserId}";
                }

                var sqlQuery = $@"SELECT [t0].* FROM [ProjectImage] AS [t0] Where [t0].[IsDeleted] = 0 {userSql} AND [t0].[Path] LIKE '%{request.SearchValue}%' ORDER BY [t0].[{request.SortColumn}] {request.SortColumnDir} OFFSET {skip} ROWS FETCH NEXT {takeA} ROWS ONLY";

                var query = await _dbContext.projectImages
                .FromSqlRaw(sqlQuery)
                .Select(u => new getOneProjectImageResponse()
                {
                    Id = u.Id,
                    Path = u.Path,
                    ProjectId = u.ProjectId,
                    CreationDate = DateTime.Parse(u.CreationDate.ToString()).ToString(),
                }).ToListAsync();

                var response = new getAllProjectImageResponse()
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
                return Success<getAllProjectImageResponse>(message: ex.Message, code: 500);
            }
        }

        public async Task<ClientResult<getOneProjectImageResponse>> getOneProjectImage(getOneRequest request)
        {
            if (request == null || request.Id <= 0)
                return Error<getOneProjectImageResponse>(message: BusinesLocalization.FillRequiredFields, code: 402);

            var existing = _ProjectImageRepository.Where(t => !t.IsDeleted && t.Id == request.Id).Select(s => new getOneProjectImageResponse
            {
                Id = s.Id,
                Path = s.Path,
                ProjectId = s.ProjectId,
                CreationDate = DateTime.Parse(s.CreationDate.ToString()).ToString(),
            }).FirstOrDefault();

            if (existing == null)
                return Error<getOneProjectImageResponse>(message: BusinesLocalization.NotFound, code: 404);

            return Success<getOneProjectImageResponse>(message: BusinesLocalization.Success, code: 200, data: existing);
        }

        public async Task<ClientResult> updateProjectImage(updateProjectImageRequest request)
        {
            if (request == null || request.Id <= 0)
                return Error(message: BusinesLocalization.FillRequiredFields, code: 402);

            var existingProjectImage = _ProjectImageRepository.FirstOrDefault(t => !t.IsDeleted && t.Id == request.Id);
            if (existingProjectImage == null)
                return Error(message: BusinesLocalization.NotFound, code: 404);

            existingProjectImage.Path = "";
            existingProjectImage.ProjectId = request.ProjectId;

            await _ProjectImageRepository.UpdateAsync(existingProjectImage, true);

            return Success(message: BusinesLocalization.UpdateSuccess, code: 200);
        }
    }
}
