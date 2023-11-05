using Microsoft.AspNetCore.Mvc;
using WhoamI.Business.Contracts.DTO.ServiceAndHobby;
using WhoamI.Business.Contracts.DTO.Client;
using WhoamI.Business.Contracts.DTO.DataTable;
using WhoamI.Business.Contracts.IManager;

namespace WhoamI_Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class ServiceAndHobbyController : BaseController
    {
        private readonly IServiceAndHobbyManager _ServiceAndHobbyManager;

        public ServiceAndHobbyController(IServiceAndHobbyManager ServiceAndHobbyManager)
        {
            _ServiceAndHobbyManager = ServiceAndHobbyManager;
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
        public async Task<JsonResult> getAllServiceAndHobby()
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

            var response = await _ServiceAndHobbyManager.getAllServiceAndHobby(request);

            return Json(response);
        }
        [HttpPost]
        public async Task<JsonResult> addServiceAndHobby(addServiceAndHobbyRequest request)
        {
            var response = await _ServiceAndHobbyManager.addServiceAndHobby(request);

            return Json(response);
        }
        [HttpPost]
        public async Task<JsonResult> getOneServiceAndHobby(getOneRequest request)
        {
            var response = await _ServiceAndHobbyManager.getOneServiceAndHobby(request);

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> deleteServiceAndHobby(getOneRequest request)
        {
            var response = await _ServiceAndHobbyManager.deleteServiceAndHobby(request);

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> updateServiceAndHobby(updateServiceAndHobbyRequest request)
        {
            var response = await _ServiceAndHobbyManager.updateServiceAndHobby(request);

            return Json(response);
        }
    }
}
