﻿using BiletPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BiletPortal.Controllers
{
   
    public class LoginController : Controller
    {

        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;


        public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager= signInManager;
            _userManager= userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TakeParameters(LoginViewModel comingParameters)
        {

            if (!ModelState.IsValid) { 
            return View("Index",comingParameters);
            
            }

            var result = await _signInManager.PasswordSignInAsync(comingParameters.UserName, comingParameters.Password,false,true);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(comingParameters.UserName);
                if(user.EmailConfirmed == true)
                {
                    return RedirectToAction("Index","Home");
                }
            }

            ModelState.AddModelError("", "Kullanıcı Adı veya parola hatalı");
            return View("Index",comingParameters);
        }
    }
}
