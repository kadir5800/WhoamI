using Microsoft.AspNetCore.Mvc;
using WhoamI.Business.Contracts.DTO.Project;
using WhoamI.Business.Contracts.DTO.Client;
using WhoamI.Business.Contracts.DTO.DataTable;
using WhoamI.Business.Contracts.IManager;
using WhoamI.Business.Contracts.DTO.Article;
using WhoamI.Business.Managers;
using Microsoft.AspNetCore.Identity;
using WhoamI.Business.Contracts.DTO.ProjectImage;

namespace WhoamI_Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class ProjectController : BaseController
    {
        private readonly IProjectManager _ProjectManager;
        private readonly IProjectImageManager _ProjectImageManager;

        public ProjectController(IProjectManager ProjectManager, IProjectImageManager projectImageManager)
        {
            _ProjectManager = ProjectManager;
            _ProjectImageManager = projectImageManager;
        }

        public IActionResult Index()
        {
            IActionResult result = RedirectToLoginIfNotConnected();
            if (result != null)
            {
                return result;
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(updateProjectRequest request, int projectId)
        {
            if (projectId > 0)
            {
                var imageReq = new addProjectImageRequest()
                {
                    file = request.file,
                    ProjectId = projectId,
                };
                var response = await _ProjectImageManager.addProjectImage(imageReq);
              
            }
            else
            {
                if (request.Id > 0)
                {
                    var response = await _ProjectManager.updateProject(request);
                   
                }
                else
                {
                    var response = await _ProjectManager.addProject(request);
                   

                }
            }
            return View();

        }
        [HttpPost]
        public async Task<JsonResult> getAllProject()
        {
            Request.Form.TryGetValue("draw", out var draw);
            Request.Form.TryGetValue("start", out var start);
            Request.Form.TryGetValue("length", out var length);
            Request.Form.TryGetValue("order[0][column]", out var order);
            Request.Form.TryGetValue("columns[" + order + "][name]", out var sortColumn);
            Request.Form.TryGetValue("order[0][dir]", out var sortColumnDir);
            Request.Form.TryGetValue("search[value]", out var searchValue);

            var request = new dataTableRequest()
            {
                Draw = draw!,
                Length = length!,
                SearchValue = searchValue!,
                SortColumn = sortColumn!,
                SortColumnDir = sortColumnDir!,
                Start = start!
            };

            var response = await _ProjectManager.getAllProject(request);

            return Json(response.Data);
        }
        [HttpPost]
        public async Task<JsonResult> getImageList(int projectId)
        {
            var request = new dataTableRequest()
            {
                Draw = "",
                Length = "1000",
                SearchValue = "",
                SortColumn = "Id",
                SortColumnDir = "",
                Start = "0",
                UserId = projectId
            };

            var userData = await _ProjectImageManager.getAllProjectImage(request);
            var response = userData.Data.data;

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> getOneProject(getOneRequest request)
        {
            var response = await _ProjectManager.getOneProject(request);

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> deleteProject(getOneRequest request)
        {
            var response = await _ProjectManager.deleteProject(request);

            return Json(response);
        }
        [HttpPost]
        public async Task<JsonResult> deletePhoto(getOneRequest request)
        {
            var response = await _ProjectImageManager.deleteProjectImage(request);

            return Json(response);
        }
    }
}
