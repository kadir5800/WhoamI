using Microsoft.AspNetCore.Mvc;
using WhoamI.Business.Contracts.DTO.Portfolio;
using WhoamI.Business.Contracts.DTO.Client;
using WhoamI.Business.Contracts.DTO.DataTable;
using WhoamI.Business.Contracts.IManager;

namespace WhoamI_Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class PortfolioController : BaseController
    {
        private readonly IPortfolioManager _PortfolioManager;

        public PortfolioController(IPortfolioManager PortfolioManager)
        {
            _PortfolioManager = PortfolioManager;
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
        public async Task<JsonResult> getAllPortfolio()
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

            var response = await _PortfolioManager.getAllPortfolio(request);

            return Json(response);
        }
        [HttpPost]
        public async Task<JsonResult> addPortfolio(addPortfolioRequest request)
        {
            var response = await _PortfolioManager.addPortfolio(request);

            return Json(response);
        }
        [HttpPost]
        public async Task<JsonResult> getOnePortfolio(getOneRequest request)
        {
            var response = await _PortfolioManager.getOnePortfolio(request);

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> deletePortfolio(getOneRequest request)
        {
            var response = await _PortfolioManager.deletePortfolio(request);

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> updatePortfolio(updatePortfolioRequest request)
        {
            var response = await _PortfolioManager.updatePortfolio(request);

            return Json(response);
        }
    }
}
