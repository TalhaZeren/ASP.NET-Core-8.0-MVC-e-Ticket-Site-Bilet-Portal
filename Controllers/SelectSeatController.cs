using BiletPortal.Component;
using BiletPortal.Data;
using BiletPortal.Dto;
using BiletPortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BiletPortal.Controllers
{
    public class SelectSeatController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly SignInManager<AppUser> _signInManager;
     

        public SelectSeatController(ApplicationDbContext context, SignInManager<AppUser> signInManager)
        {
          
            _context = context; 
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return View();
            }
            return RedirectToAction("Index", "Login");
        }
     
        [HttpPost]
        public async Task<IActionResult> SaveSeats(string seatIds)
        {
          
            if (seatIds != null && seatIds.Length > 0 )
            {
                
                    var item = await _context.Seat.FirstOrDefaultAsync(s => s.seatIdNumber == seatIds);
                    if (item != null) 
                    {
                        return Json(new { full = true });
                    }

                    var newSeat = new Seat
                    {
                        seatIdNumber = seatIds,
                        SeatNumber = "Belirli bir numara", // Bu değeri dinamik olarak belirlemek daha iyi olabilir.
                        IsBooked = true
                    };

                    _context.Seat.Add(newSeat);
                

                await _context.SaveChangesAsync();

                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Seçili değer bulunmamaktadır" });

        }
    }
}
