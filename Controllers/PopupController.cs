using BiletPortal.Data;
using BiletPortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BiletPortal.Controllers
{
    public class PopupController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;
        public PopupController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ApplicationDbContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;    
        }
        public async  Task<IActionResult> Index()
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
