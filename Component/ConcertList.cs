using BiletPortal.Data;
using Microsoft.AspNetCore.Mvc;

namespace BiletPortal.Component
{
    public class ConcertList : ViewComponent
    {

        private readonly ApplicationDbContext _context;

        public ConcertList( ApplicationDbContext context)
        {
            _context = context;
        }


        public IViewComponentResult Invoke()
        {
            //var result = _context.Products.Where(c => c.Category.CategoryName.Equals("Spor")).ToList();
            //return View(result);

            var result = _context.Products.Where(s => s.Category.CategoryName.Equals("Konser")).ToList();
            return View(result);

        }

    }
}
