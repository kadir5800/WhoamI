using Microsoft.AspNetCore.Mvc;
using WhoamI.Business.Contracts.DTO.Experince;
using WhoamI.Business.Contracts.DTO.Client;
using WhoamI.Business.Contracts.DTO.DataTable;
using WhoamI.Business.Contracts.IManager;

namespace WhoamI_Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class ExperinceController : BaseController
    {
        private readonly IExperinceManager _ExperinceManager;

        public ExperinceController(IExperinceManager ExperinceManager)
        {
            _ExperinceManager = ExperinceManager;
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
        public async Task<JsonResult> getAllExperince()
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

            var response = await _ExperinceManager.getAllExperince(request);

            return Json(response);
        }
        [HttpPost]
        public async Task<JsonResult> addExperince(addExperinceRequest request)
        {
            var response = await _ExperinceManager.addExperince(request);

            return Json(response);
        }
        [HttpPost]
        public async Task<JsonResult> getOneExperince(getOneRequest request)
        {
            var response = await _ExperinceManager.getOneExperince(request);

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> deleteExperince(getOneRequest request)
        {
            var response = await _ExperinceManager.deleteExperince(request);

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> updateExperince(updateExperinceRequest request)
        {
            var response = await _ExperinceManager.updateExperince(request);

            return Json(response);
        }
    }
}
