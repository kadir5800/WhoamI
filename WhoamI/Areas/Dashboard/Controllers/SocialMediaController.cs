using Microsoft.AspNetCore.Mvc;
using WhoamI.Business.Contracts.DTO.SocialMedia;
using WhoamI.Business.Contracts.DTO.Client;
using WhoamI.Business.Contracts.DTO.DataTable;
using WhoamI.Business.Contracts.IManager;

namespace WhoamI_Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class SocialMediaController : BaseController
    {
        private readonly ISocialMediaManager _SocialMediaManager;

        public SocialMediaController(ISocialMediaManager SocialMediaManager)
        {
            _SocialMediaManager = SocialMediaManager;
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
        public async Task<JsonResult> getAllSocialMedia()
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

            var response = await _SocialMediaManager.getAllSocialMedia(request);

            return Json(response);
        }
        [HttpPost]
        public async Task<JsonResult> addSocialMedia(addSocialMediaRequest request)
        {
            var response = await _SocialMediaManager.addSocialMedia(request);

            return Json(response);
        }
        [HttpPost]
        public async Task<JsonResult> getOneSocialMedia(getOneRequest request)
        {
            var response = await _SocialMediaManager.getOneSocialMedia(request);

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> deleteSocialMedia(getOneRequest request)
        {
            var response = await _SocialMediaManager.deleteSocialMedia(request);

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> updateSocialMedia(updateSocialMediaRequest request)
        {
            var response = await _SocialMediaManager.updateSocialMedia(request);

            return Json(response);
        }
    }
}
