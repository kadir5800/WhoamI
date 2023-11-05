using Microsoft.AspNetCore.Mvc;
using WhoamI.Business.Contracts.DTO.User;
using WhoamI.Business.Contracts.DTO.Client;
using WhoamI.Business.Contracts.DTO.DataTable;
using WhoamI.Business.Contracts.IManager;

namespace WhoamI_Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class UserController : BaseController
    {
        private readonly IUserManager _UserManager;

        public UserController(IUserManager UserManager)
        {
            _UserManager = UserManager;
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
        public async Task<JsonResult> getAllUser()
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

            var response = await _UserManager.getAllUser(request);

            return Json(response);
        }
        [HttpPost]
        public async Task<JsonResult> addUser(addUserRequest request)
        {
            var response = await _UserManager.addUser(request);

            return Json(response);
        }
        [HttpPost]
        public async Task<JsonResult> getOneUser(getOneRequest request)
        {
            var response = await _UserManager.getOneUser(request);

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> deleteUser(getOneRequest request)
        {
            var response = await _UserManager.deleteUser(request);

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> updateUser(updateUserRequest request)
        {
            var response = await _UserManager.updateUser(request);

            return Json(response);
        }
    }
}
