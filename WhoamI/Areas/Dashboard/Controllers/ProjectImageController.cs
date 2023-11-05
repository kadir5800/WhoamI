using Microsoft.AspNetCore.Mvc;
using WhoamI.Business.Contracts.DTO.ProjectImage;
using WhoamI.Business.Contracts.DTO.Client;
using WhoamI.Business.Contracts.DTO.DataTable;
using WhoamI.Business.Contracts.IManager;

namespace WhoamI_Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class ProjectImageController : BaseController
    {
        private readonly IProjectImageManager _ProjectImageManager;

        public ProjectImageController(IProjectImageManager ProjectImageManager)
        {
            _ProjectImageManager = ProjectImageManager;
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
        public async Task<JsonResult> getAllProjectImage()
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

            var response = await _ProjectImageManager.getAllProjectImage(request);

            return Json(response);
        }
        [HttpPost]
        public async Task<JsonResult> addProjectImage(addProjectImageRequest request)
        {
            var response = await _ProjectImageManager.addProjectImage(request);

            return Json(response);
        }
        [HttpPost]
        public async Task<JsonResult> getOneProjectImage(getOneRequest request)
        {
            var response = await _ProjectImageManager.getOneProjectImage(request);

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> deleteProjectImage(getOneRequest request)
        {
            var response = await _ProjectImageManager.deleteProjectImage(request);

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> updateProjectImage(updateProjectImageRequest request)
        {
            var response = await _ProjectImageManager.updateProjectImage(request);

            return Json(response);
        }
    }
}
