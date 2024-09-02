using BiletPortal.Data;
using Microsoft.AspNetCore.Mvc;

namespace BiletPortal.Component
{
    public class PopularList : ViewComponent
    {

        private readonly ApplicationDbContext _context;
        public PopularList(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var result = _context.Products.Where(s => s.Category.CategoryName.Equals("Sinema")).ToList();
            return View(result);
        }
    }
}
