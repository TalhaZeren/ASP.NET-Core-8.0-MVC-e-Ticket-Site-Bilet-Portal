using BiletPortal.Data;
using BiletPortal.Dto;
using BiletPortal.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System.Diagnostics;

namespace BiletPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public HomeController(ILogger<HomeController> logger,ApplicationDbContext context,
            UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _logger = logger;
            _context = context;
            _signInManager = signInManager;
        }
        public IActionResult Detail(int? id)
        {
            var result = _context.Products.Find(id);
            return View(result);
        }


         //Dto class will be defined for Hall method.
        public IActionResult Hall(int productId) 
        {
            //var result = _context.HallInfo.Where(P => P.ProductId == id).FirstOrDefault();
            //var result = _context.HallInfo.Where(p=> p.ProductId == productId).ToList();
            var result = _context.HallInfo.Where(s => s.Products.ProductId.Equals(productId)).ToList();
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

        [HttpPost]
        public async Task<IActionResult> SendMessage()
        {

            if (_signInManager.IsSignedIn(User))
            {
                if (User.Identity.IsAuthenticated)
                {

                    var username = User.Identity.Name;
                    var user = await _userManager.FindByNameAsync(username);

                    Random random = new Random();
                    int code = 0;
                    code = random.Next(100000, 1000000);

                    var result = _context.AppUser.FirstOrDefaultAsync(x => x.Email == user.Email);

                    if (result != null)
                    {
                        MimeMessage mimeMessage = new MimeMessage();
                        MailboxAddress mailboxAddressFrom = new MailboxAddress("Bilet Portal", "talhazeren97@gmail.com");
                        MailboxAddress mailboxAddressTo = new MailboxAddress("User", user.Email);
                        mimeMessage.From.Add(mailboxAddressFrom);
                        mimeMessage.To.Add(mailboxAddressTo);
                        BodyBuilder bodyBuilder = new BodyBuilder();
                        bodyBuilder.TextBody =  user.FirstName + " " + user.LastName +" " + user.Email + " " + "Biletiniz Baþarýyla Alýnmýþtýr." +
                            " Ýyi eðlenceler dileriz!";
                        mimeMessage.Body = bodyBuilder.ToMessageBody();
                        mimeMessage.Subject = "Bilet Portal";
                        SmtpClient smtpClient = new SmtpClient();
                        smtpClient.Connect("smtp.gmail.com", 587, false);
                        smtpClient.Authenticate("talhazeren97@gmail.com", "Your email password"); // you need to create your password in your
                                                                                                  // mail account after allowed two-step verification
                        smtpClient.Send(mimeMessage);
                        smtpClient.Disconnect(true);
                        TempData["Mail"] = user.Email;
                        return RedirectToAction("Index", "Home");
                    }

                    else
                    {
                        return NotFound();
                    }
                   
                }
              
            }
            return NotFound();
        }



    }
}
