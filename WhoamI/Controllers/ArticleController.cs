using Microsoft.AspNetCore.Mvc;

namespace WhoamI_Web.Controllers
{
    public class ArticleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
