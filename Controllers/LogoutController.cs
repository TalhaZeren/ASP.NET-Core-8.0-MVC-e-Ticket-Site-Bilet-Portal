using BiletPortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BiletPortal.Controllers
{
    public class LogoutController : Controller
    {

        private readonly SignInManager<AppUser> _signInManager;

        public LogoutController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }


        public async Task<IActionResult> Index()
        {
        await _signInManager.SignOutAsync();
            ViewBag.Mesaj = "Sistemden çıkış yapılmıştır.";
            return RedirectToAction("Index","Home");
        }
    }
}
