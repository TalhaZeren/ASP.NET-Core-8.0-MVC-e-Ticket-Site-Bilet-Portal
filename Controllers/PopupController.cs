using Microsoft.AspNetCore.Mvc;

namespace BiletPortal.Controllers
{
    public class PopupController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PopupForLogout()
        {
         return  View();
        }

        public IActionResult AddToCardAndContinue()
        {
            return View();
        }

    }
}
