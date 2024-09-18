//using BiletPortal.Data;
//using MailKit.Search;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace BiletPortal.Controllers
//{
//    public class HallController : Controller
//    {

//        private readonly ApplicationDbContext _context;

//        public HallController(ApplicationDbContext context)
//        {
//            _context = context;
//        }
//        public async Task<IActionResult> Index(string searchTerm)
//        {
//            var applicationDbContext = _context.Products.Include(p => p.Category); // Eager Loading
//            var model = await applicationDbContext.ToListAsync();
//            if (!string.IsNullOrEmpty(searchTerm))
//            {
//                model = await _context.Products.Where(p => p.ProductName.ToLower().Contains(searchTerm)).ToListAsync();
//                return PartialView("_ProductList", model);
//            }
//            else
//            {
//                return View(model);
//            }
//        }
//    }
//}
