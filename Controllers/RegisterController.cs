using BiletPortal.Dto;
using BiletPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MimeKit;
using System.Net.Mail;
using MailKit.Net.Smtp;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace BiletPortal.Controllers
{
    public class RegisterController : Controller
    {

        private readonly UserManager<AppUser> _userManager;

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(AppUserRegisterDto appUserRegisterDto)
        {
            Random random = new Random();
            int code = 0;
            code = random.Next(100000, 1000000);

            AppUser appUser = new AppUser()
            {
                FirstName = appUserRegisterDto.FirstName,
                LastName = appUserRegisterDto.LastName,
                City = appUserRegisterDto.City,
                Email = appUserRegisterDto.Email,
                UserName = appUserRegisterDto.UserName,
                ConfirmCode = code,
            };

            if (!ModelState.IsValid) 
            {

                var tempMail = await _userManager.FindByEmailAsync(appUserRegisterDto.Email);

                var tempName = await _userManager.FindByNameAsync(appUserRegisterDto.UserName);
                if (tempMail != null)
                {
                    ModelState.AddModelError("", "Girdiğiniz mail adresi zaten kullanılıyor.");
                    return View(appUserRegisterDto);
                }
                if (tempName != null) 
                {
                    ModelState.AddModelError("", "Girdiğiniz kullanıcı adı zaten kullanılıyor.");
                   return View(appUserRegisterDto);
                }
                if (appUserRegisterDto.Password != appUserRegisterDto.ConfirmPassword)
                {
                    ModelState.AddModelError("", "Parolalar uyuşmuyor.");
                    return View(appUserRegisterDto);
                }
            }
            var result = await _userManager.CreateAsync(appUser,appUserRegisterDto.Password);

            if (result.Succeeded)
            {
                MimeMessage mimeMessage = new MimeMessage();    
                MailboxAddress mailBoxAddressFrom = new MailboxAddress("Bilet Portal","talhazeren97@gmail.com");
                MailboxAddress mailBoxAdressTo = new MailboxAddress("User", appUser.Email);
                mimeMessage.From.Add(mailBoxAddressFrom);
                mimeMessage.To.Add(mailBoxAdressTo);
                BodyBuilder bodybuilder = new BodyBuilder();
                bodybuilder.TextBody = "Kaydınız başarıyla gerçekleşti.Onay Kodunuz : " + code;
                mimeMessage.Body = bodybuilder.ToMessageBody();
                mimeMessage.Subject = "Bilet Portal";
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Connect("smtp.gmail.com",587,false);
                smtpClient.Authenticate("talhazeren97@gmail.com", "vwub jxar zcru ieqq");
                smtpClient.Send(mimeMessage);
                smtpClient.Disconnect(true);
                TempData["Mail"] = appUserRegisterDto.Email;
                return RedirectToAction("Index", "ConfirmMail");

            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", "Lütfen kullanıcı bilgilerini doğru giriniz.");
                    return View("Index", appUserRegisterDto);
                }
            }
            ModelState.AddModelError("", "Lütfen kullanıcı bilgilerini doğru giriniz.");
            return View(appUserRegisterDto);
        
        }  
    }
}
//vwub jxar zcru ieqq

//Talha&.44