using Microsoft.AspNetCore.Mvc;

namespace WhoamI_Web.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
