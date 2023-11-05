using Microsoft.AspNetCore.Mvc;
using WhoamI.Business.Contracts.DTO.Ability;
using WhoamI.Business.Contracts.DTO.Client;
using WhoamI.Business.Contracts.DTO.DataTable;
using WhoamI.Business.Contracts.IManager;

namespace WhoamI_Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class AbilityController : BaseController
    {
        private readonly IAbilityManager _abilityManager;

        public AbilityController(IAbilityManager abilityManager)
        {
            _abilityManager = abilityManager;
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
        public async Task<JsonResult> getAllAbility()
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

            var response = await _abilityManager.getAllAbility(request);

            return Json(response);
        }
        [HttpPost]
        public async Task<JsonResult> addAbility(addAbilityRequest request)
        {
            var response = await _abilityManager.addAbility(request);

            return Json(response);
        }
        [HttpPost]
        public async Task<JsonResult> getOneAbility(getOneRequest request)
        {
            var response = await _abilityManager.getOneAbility(request);

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> deleteAbility(getOneRequest request)
        {
            var response = await _abilityManager.deleteAbility(request);

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> updateAbility(updateAbilityRequest request)
        {
            var response = await _abilityManager.updateAbility(request);

            return Json(response);
        }
    }
}
