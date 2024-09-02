using BiletPortal.Data;
using Microsoft.AspNetCore.Mvc;

namespace BiletPortal.Component
{
    public class StandUpList : ViewComponent
    {

        private readonly ApplicationDbContext _context;

        public StandUpList(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var result = _context.Products.Where(m => m.Category.CategoryName.Equals("Stand Up")).ToList();
            return View(result);
        }

    }
}
