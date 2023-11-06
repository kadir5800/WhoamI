using Microsoft.AspNetCore.Mvc;
using WhoamI.Business.Contracts.DTO.UserContact;
using WhoamI.Business.Contracts.DTO.Client;
using WhoamI.Business.Contracts.DTO.DataTable;
using WhoamI.Business.Contracts.IManager;

namespace WhoamI_Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class HomeController : BaseController
    {
        private readonly IUserContactManager _UserContactManager;

        public HomeController(IUserContactManager UserContactManager)
        {
            _UserContactManager = UserContactManager;
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
        public async Task<JsonResult> getAllUserContact()
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

            var response = await _UserContactManager.getAllUserContact(request);

            return Json(response.Data);
        }
        [HttpPost]
        public async Task<JsonResult> addUserContact(addUserContactRequest request)
        {
            var response = await _UserContactManager.addUserContact(request);

            return Json(response);
        }
        [HttpPost]
        public async Task<JsonResult> getOneUserContact(getOneRequest request)
        {
            var response = await _UserContactManager.getOneUserContact(request);

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> deleteUserContact(getOneRequest request)
        {
            var response = await _UserContactManager.deleteUserContact(request);

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> updateUserContact(updateUserContactRequest request)
        {
            var response = await _UserContactManager.updateUserContact(request);

            return Json(response);
        }
    }
}
