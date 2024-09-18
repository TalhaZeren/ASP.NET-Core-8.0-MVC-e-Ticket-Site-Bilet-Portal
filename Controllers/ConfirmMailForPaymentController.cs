using BiletPortal.Data;
using BiletPortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BiletPortal.Controllers
{
    public class ConfirmMailForPaymentController : Controller
    {

        private readonly ApplicationDbContext _context;
        public ConfirmMailForPaymentController(ApplicationDbContext context)
        {
            _context = context;   
        }


        [HttpGet]   
        public IActionResult Index()
        {   
            var value = TempData["Mail"];       
            ViewBag.Email = value; 
            return View();      // return to ConfirmMail Page for Payment
        }

        [HttpPost]
        public async Task<IActionResult> TakeParamtersPayment(ConfirmPayment confirmPayment)
        {

            var payment = await _context.Payment.FirstOrDefaultAsync(p => p.Email == confirmPayment.Email);

            if (payment != null)
            {
                if (payment.Email == confirmPayment.Email)
                {
                    payment.IsSuccessful = true;
                    HttpContext.Session.Remove("Card");
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Popup");
                }
            }
            return View();
            
        }

    }
}
