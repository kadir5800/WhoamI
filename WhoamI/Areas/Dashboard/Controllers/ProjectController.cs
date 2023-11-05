using Microsoft.AspNetCore.Mvc;
using WhoamI.Business.Contracts.DTO.Project;
using WhoamI.Business.Contracts.DTO.Client;
using WhoamI.Business.Contracts.DTO.DataTable;
using WhoamI.Business.Contracts.IManager;

namespace WhoamI_Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class ProjectController : BaseController
    {
        private readonly IProjectManager _ProjectManager;

        public ProjectController(IProjectManager ProjectManager)
        {
            _ProjectManager = ProjectManager;
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

            return Json(response);
        }
        [HttpPost]
        public async Task<JsonResult> addProject(addProjectRequest request)
        {
            var response = await _ProjectManager.addProject(request);

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
        public async Task<JsonResult> updateProject(updateProjectRequest request)
        {
            var response = await _ProjectManager.updateProject(request);

            return Json(response);
        }
    }
}
