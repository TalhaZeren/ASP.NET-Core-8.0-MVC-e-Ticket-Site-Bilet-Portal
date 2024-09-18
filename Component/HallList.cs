using BiletPortal.Data;
using Microsoft.AspNetCore.Mvc;

namespace BiletPortal.Component
{
    public class HallList :ViewComponent
    {

        private readonly ApplicationDbContext _context;
        public HallList(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var result = _context.HallInfo.ToList();
            return View(result);
        }

    }
}
