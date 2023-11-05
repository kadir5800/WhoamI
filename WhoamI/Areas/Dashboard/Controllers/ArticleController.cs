using Microsoft.AspNetCore.Mvc;
using WhoamI.Business.Contracts.DTO.Article;
using WhoamI.Business.Contracts.DTO.Client;
using WhoamI.Business.Contracts.DTO.DataTable;
using WhoamI.Business.Contracts.IManager;

namespace WhoamI_Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class ArticleController : BaseController
    {
        private readonly IArticleManager _ArticleManager;

        public ArticleController(IArticleManager ArticleManager)
        {
            _ArticleManager = ArticleManager;
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
        public async Task<JsonResult> getAllArticle()
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

            var response = await _ArticleManager.getAllArticle(request);

            return Json(response);
        }
        [HttpPost]
        public async Task<JsonResult> addArticle(addArticleRequest request)
        {
            var response = await _ArticleManager.addArticle(request);

            return Json(response);
        }
        [HttpPost]
        public async Task<JsonResult> getOneArticle(getOneRequest request)
        {
            var response = await _ArticleManager.getOneArticle(request);

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> deleteArticle(getOneRequest request)
        {
            var response = await _ArticleManager.deleteArticle(request);

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> updateArticle(updateArticleRequest request)
        {
            var response = await _ArticleManager.updateArticle(request);

            return Json(response);
        }
    }
}
