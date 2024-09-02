using BiletPortal.Data;
using BiletPortal.Models;

using MailKit.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BiletPortal.Controllers
{
    public class SearchController : Controller
    {

        private readonly ApplicationDbContext _context;
      

        public SearchController(ApplicationDbContext context)
        {
            _context = context;
         

        }
        [HttpGet]
        public async Task<IActionResult> SearchProduct(string searchTerm)
        {
            List<Products> products = new List<Products>();

            if (string.IsNullOrEmpty(searchTerm))  // was checked null or non-null
            {
                return View(products);
            }

            var item = await _context.Products.Where(p => p.ProductName.StartsWith(searchTerm)).ToListAsync();
            return View(item);
        }


        [HttpGet]
        public async Task<IActionResult> SearchCategory(string searchTerm)
        {
            
          
            if (string.IsNullOrEmpty(searchTerm))  // was checked null or non-null
            {
                return View(new List<Category>());
            }

            var item = await _context.Category.Where(p => p.CategoryName.StartsWith(searchTerm)).ToListAsync();
            return View(item);
        }



        [HttpGet]
        public async Task<IActionResult> SearchSlider(string searchTerm) { 


    
            if (string.IsNullOrEmpty(searchTerm))  // was checked null or non-null
            {
                return View(new List<Slider>());
            }

            var item = await _context.Slider.Where(p => p.SliderName.StartsWith(searchTerm)).ToListAsync();
            return View(item);

        }
    }
}
