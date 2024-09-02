using BiletPortal.Data;
using Microsoft.AspNetCore.Mvc;

namespace BiletPortal.Component
{
    public class TheatreList : ViewComponent
    {

        private readonly ApplicationDbContext _context;
        public TheatreList(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var result = _context.Products.Where(s => s.Category.CategoryName.Equals("Tiyatro")).ToList();
            return View(result);
        }
    }
}
