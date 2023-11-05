using Microsoft.AspNetCore.Mvc;

namespace WhoamI_Web.Areas.Dashboard.Controllers
{
    public class BaseController : Controller
    {
        protected bool IsUserConnected()
        {
            return HttpContext.Session.GetInt32("isConnected") == 1;
        }

        protected IActionResult RedirectToLoginIfNotConnected()
        {
            if (!IsUserConnected())
            {
                return RedirectToAction("Index", "Login", new { area = "Dashboard" });
            }

            return null;
        }
    }
}
