using BiletPortal.Data;
using BiletPortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class SelectSeatController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly SignInManager<AppUser> _signInManager;

    public SelectSeatController(ApplicationDbContext context, SignInManager<AppUser> signInManager)
    {
        _context = context;
        _signInManager = signInManager;
    }

    public IActionResult Index(int productId)
    {
        if (_signInManager.IsSignedIn(User))
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product == null)
            {
                return NotFound("Ürün bulunamadı.");
            }

            var model = new SelectSeat
            {
                ProductId = productId,
                Products = product
            };

            return View(model);
        }

        return RedirectToAction("Index", "Login");
    }

    [HttpPost]
    public async Task<IActionResult> SaveSeats(string seatId, int productId)
    {
        Random random = new Random();
        if (_signInManager.IsSignedIn(User))
        {
           
            var user = await _signInManager.UserManager.GetUserAsync(User); // Kullanıcı bilgilerini al


            if (!string.IsNullOrEmpty(seatId) && productId > 0)
            {
                var seat = await _context.SelectSeat.FirstOrDefaultAsync(s => s.seatIdNumber == seatId && s.ProductId == productId);
                if (seat != null)
                {
                    return Json(new { success = false, message = "Koltuk zaten dolu!" });
                }

                var newSeat = new SelectSeat
                {
                    seatIdNumber = seatId,
                    ProductId = productId,
                    SeatNumber = "SeatNumber",
                    IsBooked = true,
                    UserId = user.Id// Kullanıcıyla ilişkilendir
                };

                _context.SelectSeat.Add(newSeat);
                await _context.SaveChangesAsync();

                return Json(new { success = true });
            }
        }

        return Json(new { success = false, message = "Geçersiz koltuk veya ürün bilgisi." });
    }
}
