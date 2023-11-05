using Microsoft.AspNetCore.Mvc;
using WhoamI.Business.Contracts.DTO.Authentication;
using WhoamI.Business.Contracts.IManager;
using WhoamI_Web.Areas.Dashboard.Models;

namespace WhoamI_Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class LoginController : BaseController
    {
        private readonly IAuthenticationManager _authenticationManager;

        public LoginController(IAuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("isConnected") == 1)
            {
                return RedirectToAction("Index", "Home");
            }
           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var request = new loginRequest() {
                password = model.Password,
                userName=model.UserName,
                };
                var response = await _authenticationManager.login(request);
                if (response.Success)
                {
                    HttpContext.Session.SetInt32("isConnected", 1);
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Giriş başarısız.");
            }

            return View(model);
        }
        public IActionResult Logout()
        {
            try
            {
                HttpContext.Session.SetInt32("isConnected", 0);
                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = ex.Message.ToString() });

            }

        }
    }
}
