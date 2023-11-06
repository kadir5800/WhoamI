using Microsoft.AspNetCore.Mvc;

namespace WhoamI_Web.Controllers
{
    public class ProjectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
