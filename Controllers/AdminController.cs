using Microsoft.AspNetCore.Mvc;

namespace BiletPortal.Controllers
{
    public class AdminController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
