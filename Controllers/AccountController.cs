using BiletPortal.Dto;
using BiletPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BiletPortal.Controllers   
{

    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;   
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var value = await _userManager.FindByNameAsync(User.Identity.Name);
            AppUserEditDto appUserEditDto = new AppUserEditDto()
            {
                FirstName = value.FirstName,
                LastName = value.LastName,
                PhoneNumber = value.PhoneNumber,
                City = value.City,
                Email = value.Email,
            };  
            return View(appUserEditDto);
        }


        [HttpPost]
        public async Task<IActionResult> Arrangement(AppUserEditDto appUserEditDto)
        {
            if(appUserEditDto.Password == appUserEditDto.ConfirmPassword)
            {
                var user = await _userManager.FindByEmailAsync(appUserEditDto.Email);
                user.FirstName = appUserEditDto.FirstName; 
                user.LastName = appUserEditDto.LastName;
                user.City= appUserEditDto.City;
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, appUserEditDto.Password);

                var result = await _userManager.UpdateAsync(user);

                if(result.Succeeded)
                {
                    return RedirectToAction("Index", "Account");
                }
            }
            return View();
        }
    }
}
