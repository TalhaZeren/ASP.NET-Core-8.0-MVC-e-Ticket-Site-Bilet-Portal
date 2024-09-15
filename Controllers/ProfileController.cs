using BiletPortal.Data;
using BiletPortal.Dto;
using BiletPortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BiletPortal.Controllers
{
    public class ProfileController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager; 

        public ProfileController(ApplicationDbContext context, UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (_signInManager.IsSignedIn(User))
            {
                if (User.Identity.IsAuthenticated)
                {

                    var username = User.Identity.Name;
                    var user = await _userManager.FindByNameAsync(username);

                    if (user != null)
                    {
                        var tickets = await _context.SelectSeat
                         .Where(t => t.UserId == user.Id)
                             .Include(t => t.Products) // Ürün bilgisi ile ilişkilendir
                               .ToListAsync();
                       

                        var viewModel = new ProfileViewModel()
                        {
                            AppUser = user,    // Kullanıcı bilgilerini taşı
                            Tickets = tickets ?? new List<SelectSeat>() // Bilet bilgilerini taşı
                        };
               
                        return View(viewModel);
                    }       
                    else
                    {
                        return View("Index", "Home");
                    }

                }
            }
            return View("Index", "Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(string email, [Bind("FirstName,LastName,Email,City")] AppUser appUser)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var userUpdate = await _userManager.FindByEmailAsync(email);

                    if (userUpdate != null)
                    {
                        userUpdate.FirstName = appUser.FirstName;
                        userUpdate.LastName = appUser.LastName;
                        userUpdate.Email = appUser.Email;
                        userUpdate.City = appUser.City;

                        var result = await _userManager.UpdateAsync(userUpdate);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index","Profile");
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while updating the user.");
                }
            }
            return View(appUser);
        }


        [HttpGet]
        public async Task<IActionResult> Ticket()
        {
            if (_signInManager.IsSignedIn(User))
            {
                if (User.Identity.IsAuthenticated)
                {

                    var username = User.Identity.Name;
                    var user = await _userManager.FindByNameAsync(username);

                    if (user != null)
                    {
                        var tickets = await _context.SelectSeat
                         .Where(t => t.UserId == user.Id)
                             .Include(t => t.Products) // Ürün bilgisi ile ilişkilendir
                               .ToListAsync();

                        var viewModel = new ProfileViewModel
                        {
                            AppUser = user,    // Kullanıcı bilgilerini taşı
                            Tickets = tickets ?? new List<SelectSeat>() // Bilet bilgilerini taşı
                        };

                        return View(viewModel);
                    }
                    else
                    {
                        return View("Index", "Home");
                    }

                }
            }
            return View("Index", "Login");
        }




    }
}
