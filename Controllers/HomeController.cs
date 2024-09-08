using BiletPortal.Data;
using BiletPortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BiletPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public HomeController(ILogger<HomeController> logger,ApplicationDbContext context,UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _logger = logger;
            _context = context;
        }
        public IActionResult Detail(int? id)
        {
            var result = _context.Products.Find(id);
            return View(result);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Deneme()
        {
            return View();
        }
    }
}
