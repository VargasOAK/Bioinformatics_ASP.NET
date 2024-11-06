using Microsoft.AspNetCore.Mvc;

namespace YourProject.Controllers
{
    public class AccountController : Controller
    {
        [Route("/Account/Login")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("/Account/Register")]
        public IActionResult Register()
        {
            return View();
        }

        [Route("/Account/Logout")]
        public IActionResult Logout()
        {
            // Add logout logic
            return RedirectToAction("Index", "Home");
        }
    }
}
