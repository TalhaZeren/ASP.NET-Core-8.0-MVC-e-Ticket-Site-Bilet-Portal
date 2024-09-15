using BiletPortal.Data;
using BiletPortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BiletPortal.Component
{
    public class InformationList : ViewComponent
    {

        private readonly ApplicationDbContext _context;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        public InformationList(ApplicationDbContext context, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {

            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
           
      

            return View();
        }

    }
}
