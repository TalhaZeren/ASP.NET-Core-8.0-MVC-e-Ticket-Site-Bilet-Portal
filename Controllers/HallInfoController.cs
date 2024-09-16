using BiletPortal.Data;
using BiletPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BiletPortal.Controllers
{
    public class HallInfoController : Controller
    {

        private readonly ApplicationDbContext _context;

        public HallInfoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? searchTerm)
        {
            var model = await _context.HallInfo.ToListAsync();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                model = await _context.HallInfo.Where(s => s.HallName.ToLower().Contains(searchTerm)).ToListAsync();
                return PartialView("_HallList", model);
            }
            return View(model);
        }

        // GET: HallInfo/Details/5
        public async Task<IActionResult>
            Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hall = await _context.HallInfo
            .FirstOrDefaultAsync(m => m.Hallid == id);
            if (hall == null)
            {
                return NotFound();
            }

            return View(hall);
        }

        // GET: HallInfo/Create
        public IActionResult Create()
        {
            /*     ViewData["CategoryId"] = new SelectList(_context.Category,  // Created dropdownList to choose.
                                             "CategoryId", "CategoryId");*/
            ViewBag.productList = new SelectList(_context.Products, "ProductId", "ProductName");
            return View();
        }

        // POST: HallInfo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
            Create([Bind("Hallid,HallName,City,LocationInformation,Date,Time,ProductId")] HallInfo hallInfo)
        {

            if (ModelState.IsValid) // Necessary fields is validated.
            {
                _context.Add(hallInfo); // After that marked to add database.
                await _context.SaveChangesAsync(); // added to database
                return RedirectToAction(nameof(Index)); // Redirect to product page.
            }
            ViewData["ProductId"] = new SelectList(_context.Products, // If adding proccess failed.                                                 
                "ProductId", "ProductId", hallInfo.ProductId);
            return View(hallInfo);
        }
        // If adding proccess failed.   
        // this method and under this method which is name 'return View(product)'
        // returned to beginning page (product adding fields)

        // GET: HallInfo/Edit/5
        public async Task<IActionResult>
            Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hallInfo = await _context.HallInfo.FindAsync(id);
            if (hallInfo == null)
            {
                return NotFound();
            }
            return View(hallInfo);
        }

        // POST: HallInfo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
            Edit(int id, [Bind("Hallid,HallName,City,LocationInformation,Date,Time,ProductId")] HallInfo hallInfo)
        {
            



            if (id != hallInfo.Hallid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hallInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HallInfoExists(hallInfo.Hallid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(hallInfo);
        }

        // GET: HallInfo/Delete/5
        public async Task<IActionResult>
            Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hallInfo = await _context.HallInfo
            .FirstOrDefaultAsync(m => m.Hallid == id);
            if (hallInfo == null)
            {
                return NotFound();
            }

            return View(hallInfo);
        }

        // POST: HallInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
        DeleteConfirmed(int id)
        {

            var hallInfo = await _context.HallInfo.FindAsync(id);
            if (hallInfo != null)
            {
                _context.HallInfo.Remove(hallInfo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HallInfoExists(int id)
        {
            return _context.HallInfo.Any(e => e.Hallid == id);
        }
    }
}
    

