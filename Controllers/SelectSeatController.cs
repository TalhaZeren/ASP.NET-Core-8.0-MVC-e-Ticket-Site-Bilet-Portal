using BiletPortal.Data;
using BiletPortal.Dto;
using BiletPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BiletPortal.Controllers
{
    public class SelectSeatController : Controller
    {

        private readonly ApplicationDbContext _context;

        public SelectSeatController(ApplicationDbContext context)
        {
            _context = context; 
        }

       
        public IActionResult Index()
        {
            return View();
        }
     
        [HttpPost]
        public async Task<IActionResult> SaveSeats(string[] seatIds)
        {

            if (seatIds != null && seatIds.Length > 0 )
            {
                
                foreach (var seatId in seatIds)
                {
                    var item = await _context.Seat.FirstOrDefaultAsync(s => s.seatIdNumber == seatId);
                    if (item != null) 
                    {
                        return Json(new { full = true });
                    }

                    var newSeat = new Seat
                    {
                        seatIdNumber = seatId,
                        SeatNumber = "Belirli bir numara", // Bu değeri dinamik olarak belirlemek daha iyi olabilir.
                        IsBooked = true
                    };

                    _context.Seat.Add(newSeat);
                }

                await _context.SaveChangesAsync();

                return Json(new { success = true });
            }

            
            return Json(new { success = false, message = "Seçili değer bulunmamaktadır" });

        }




    }
}
