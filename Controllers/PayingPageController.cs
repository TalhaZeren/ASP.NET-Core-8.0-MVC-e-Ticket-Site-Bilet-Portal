using BiletPortal.Data;
using BiletPortal.Dto;
using BiletPortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System.Net.Mail;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
namespace BiletPortal.Controllers
{
    public class PayingPageController : Controller
    {
        private readonly ApplicationDbContext _context;
   
        public PayingPageController(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
          
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
                    

        [HttpPost]
        public async Task<IActionResult> TakePayment(PaymentDto paymentDto)
        {
            Random random = new Random();
            int code = 0;
            code = random.Next(100000, 1000000);

            Payment payment = new Payment()
            {
                CardHolderName = paymentDto.CardHolderName,
                ExpirationDateMonth = paymentDto.ExpirationDateMonth,
                ExpirationDateYear = paymentDto.ExpirationDateYear,
                CVV = paymentDto.CVV,
                TransactionId = code,
                Email = paymentDto.Email,
                CardNumber = paymentDto.CardNumber,
            };

            await _context.AddAsync(payment);
            await _context.SaveChangesAsync();
            var result = _context.Payment.FirstOrDefaultAsync(x => x.Email == paymentDto.Email);

            if (result != null)
            {
                MimeMessage mimeMessage = new MimeMessage();
                MailboxAddress mailboxAddressFrom = new MailboxAddress("Bilet Portal", "talhazeren97@gmail.com");
                MailboxAddress mailboxAddressTo = new MailboxAddress("User", payment.Email);
                mimeMessage.From.Add(mailboxAddressFrom);
                mimeMessage.To.Add(mailboxAddressTo);
                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.TextBody = "Ödeme işleminiz için onay kodu : " + code;
                mimeMessage.Body = bodyBuilder.ToMessageBody();
                mimeMessage.Subject = "Bilet Portal";
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Connect("smtp.gmail.com", 587, false);
                smtpClient.Authenticate("talhazeren97@gmail.com", "vwub jxar zcru ieqq");
                smtpClient.Send(mimeMessage);
                smtpClient.Disconnect(true);
                TempData["Mail"] = payment.Email;
                return RedirectToAction("Index", "ConfirmMailForPayment");
            }

            else
            {
                return NotFound();
            }

        }
    }
}
