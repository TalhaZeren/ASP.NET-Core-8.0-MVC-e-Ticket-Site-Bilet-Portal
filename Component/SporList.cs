using BiletPortal.Data;
using Microsoft.AspNetCore.Mvc;

namespace BiletPortal.Component
{
    public class SporList : ViewComponent
    {

        private readonly ApplicationDbContext _context;


        public SporList(ApplicationDbContext context)
        {
         _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var result = _context.Products.Where(s => s.Category.CategoryName.Equals("Spor")).ToList();
            return View(result);
        }

    }
}
