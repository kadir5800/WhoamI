using Microsoft.AspNetCore.Mvc;
using WhoamI.Business.Contracts.DTO.Education;
using WhoamI.Business.Contracts.DTO.Client;
using WhoamI.Business.Contracts.DTO.DataTable;
using WhoamI.Business.Contracts.IManager;

namespace WhoamI_Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class EducationController : BaseController
    {
        private readonly IEducationManager _EducationManager;

        public EducationController(IEducationManager EducationManager)
        {
            _EducationManager = EducationManager;
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
        public async Task<JsonResult> getAllEducation()
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

            var response = await _EducationManager.getAllEducation(request);

            return Json(response);
        }
        [HttpPost]
        public async Task<JsonResult> addEducation(addEducationRequest request)
        {
            var response = await _EducationManager.addEducation(request);

            return Json(response);
        }
        [HttpPost]
        public async Task<JsonResult> getOneEducation(getOneRequest request)
        {
            var response = await _EducationManager.getOneEducation(request);

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> deleteEducation(getOneRequest request)
        {
            var response = await _EducationManager.deleteEducation(request);

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> updateEducation(updateEducationRequest request)
        {
            var response = await _EducationManager.updateEducation(request);

            return Json(response);
        }
    }
}
