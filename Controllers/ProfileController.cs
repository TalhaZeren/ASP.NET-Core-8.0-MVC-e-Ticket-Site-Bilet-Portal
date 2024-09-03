using BiletPortal.Data;
using BiletPortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BiletPortal.Controllers
{
    public class ProfileController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;


        public ProfileController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                string username = User.Identity.Name;
                var user = await _userManager.FindByNameAsync(username);

                if (user != null)
                {
                    return View(user);
                }
                else
                {
                    return View("Index", "Home");
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
        
    }
}
