using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoamI.Business.Contracts.DTO.Project;
using WhoamI.Business.Contracts.DTO.Client;
using WhoamI.Business.Contracts.DTO.DataTable;
using WhoamI.Business.Contracts.IManager;
using WhoamI.Business.Localization;
using WhoamI.Data.Contracts.Repositories;
using WhoamI.Data.EntityFrameworkCore;
using WhoamI.Data.Entitys.Objects;
using Azure.Core;
using Microsoft.EntityFrameworkCore;

namespace WhoamI.Business.Managers
{
    public class ProjectManager : BaseManager, IProjectManager
    {
        private readonly IProjectRepository _ProjectRepository;
        private readonly WhoamIDbContext _dbContext;
        private readonly PhotoManager _photoManager;
        public ProjectManager(IProjectRepository ProjectRepository, WhoamIDbContext dbContext, PhotoManager photoManager)
        {
            _ProjectRepository = ProjectRepository;
            _dbContext = dbContext;
            _photoManager = photoManager;
        }

        public async Task<ClientResult> addProject(addProjectRequest request)
        {

            if (request == null || String.IsNullOrEmpty(request.Description) || String.IsNullOrEmpty(request.Name))
                return Error(message: BusinesLocalization.FillRequiredFields, code: 402);


            var existingProvince = _ProjectRepository.FirstOrDefault(t => t.Name == request.Name && t.Description == request.Description && t.UserId == request.UserId);

          

            if (existingProvince != null)
                return Error(message: BusinesLocalization.sameRecordAvailable, code: 402);

            request.Icon = "";
            if (request.file != null)
            {
                var path = _photoManager.UploadPhoto(request.file, "ProjectIcon");
                if (path != null && !String.IsNullOrEmpty(path))
                {
                    request.Icon = path;
                }
                else
                {
                    request.Icon = "";
                }
            }
            await _ProjectRepository.InsertAsync(new Project()
            {
                UserId = request.UserId,
                Name = request.Name,
                Description = request.Description,
                Icon = request.Icon,
                WebAddress = request.WebAddress,
                CreationDate = DateTime.Now,
                IsDeleted = false
            }, true);

            return Success(message: BusinesLocalization.InsertSuccess, code: 200);

        }

        public async Task<ClientResult> deleteProject(getOneRequest request)
        {
            if (request == null || request.Id <= 0)
                return Error(message: BusinesLocalization.FillRequiredFields, code: 402);

            var existingProvince = _ProjectRepository.FirstOrDefault(t => !t.IsDeleted && t.Id == request.Id);

            if (existingProvince == null)
                return Error(message: BusinesLocalization.NotFound, code: 404);

            existingProvince.IsDeleted = true;

            await _ProjectRepository.UpdateAsync(existingProvince, true);
            _photoManager.DeletePhoto(existingProvince.Icon);
           
            return Success(message: BusinesLocalization.DeleteSuccess, code: 200);
        }

        public async Task<ClientResult<getAllProjectResponse>> getAllProject(dataTableRequest request)
        {
            try
            {
                if (request == null)
                    return Error<getAllProjectResponse>(message: BusinesLocalization.FillRequiredFields, code: 402);

                int pageSize = request.Length != null ? Convert.ToInt32(request.Length) : 0;
                int skip = request.Start != null ? Convert.ToInt32(request.Start) : 0;

                int recordsTotal = 0;

                recordsTotal = _ProjectRepository.Where(w => w.IsDeleted == false).Count();

                var takeA = request.Length == "-1" ? recordsTotal : pageSize;
                takeA = takeA == 0 ? 10 : takeA;

                var userSql = "";
                if (request.UserId > 0)
                {
                    userSql = $" AND [t0].[UserId]= {request.UserId}";
                }

                var sqlQuery = $@"SELECT [t0].* FROM [Project] AS [t0] Where [t0].[IsDeleted] = 0 {userSql} AND [t0].[Name] LIKE '%{request.SearchValue}%' ORDER BY [t0].[{request.SortColumn}] {request.SortColumnDir} OFFSET {skip} ROWS FETCH NEXT {takeA} ROWS ONLY";

                var query = await _dbContext.projects
                .FromSqlRaw(sqlQuery)
                .Select(u => new getOneProjectResponse()
                {
                    Id = u.Id,
                    Name = u.Name,
                    Description = u.Description,
                    UserId = u.UserId,
                    Icon = u.Icon,
                    WebAddress = u.WebAddress,
                    CreationDate = DateTime.Parse(u.CreationDate.ToString()).ToString(),
                }).ToListAsync();

                var response = new getAllProjectResponse()
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
                return Success<getAllProjectResponse>(message: ex.Message, code: 500);
            }
        }

        public async Task<ClientResult<getOneProjectResponse>> getOneProject(getOneRequest request)
        {
            if (request == null || request.Id <= 0)
                return Error<getOneProjectResponse>(message: BusinesLocalization.FillRequiredFields, code: 402);

            var existingBank = _ProjectRepository.Where(t => !t.IsDeleted && t.Id == request.Id).Select(s => new getOneProjectResponse
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                UserId = s.UserId,
                Icon = s.Icon,
                WebAddress = s.WebAddress,
                CreationDate = DateTime.Parse(s.CreationDate.ToString()).ToString(),
            }).FirstOrDefault();

            if (existingBank == null)
                return Error<getOneProjectResponse>(message: BusinesLocalization.NotFound, code: 404);

            return Success<getOneProjectResponse>(message: BusinesLocalization.Success, code: 200, data: existingBank);
        }

        public async Task<ClientResult> updateProject(updateProjectRequest request)
        {
            if (request == null || request.Id <= 0)
                return Error(message: BusinesLocalization.FillRequiredFields, code: 402);

            var existingProject = _ProjectRepository.FirstOrDefault(t => !t.IsDeleted && t.Id == request.Id);
            if (existingProject == null)
                return Error(message: BusinesLocalization.NotFound, code: 404);

            if (request.file!=null)
            {
                var path = _photoManager.UploadPhoto(request.file, "ProjectIcon");
                if (path != null)
                {
                    request.Icon= path;
                }
                else
                {
                    request.Icon=existingProject.Icon;
                }
            }
            else
            {
                request.Icon = existingProject.Icon;
            }
           


            existingProject.Name = request.Name;
            existingProject.WebAddress = request.WebAddress;
            existingProject.Icon = request.Icon;
            existingProject.Description = request.Description;
            existingProject.UserId = request.UserId;

            await _ProjectRepository.UpdateAsync(existingProject, true);

            return Success(message: BusinesLocalization.UpdateSuccess, code: 200);
        }
    }
}
