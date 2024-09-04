using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BiletPortal.Data;
using BiletPortal.Models;
using System.Security.Principal;
using MailKit.Search;


namespace BiletPortal.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;


        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
     

        }

        // GET Process for Prodcut Class.
        [HttpGet]
        public async Task<IActionResult> Index(string? searchTerm) // the program will be programed as asynchronous. This state provides to wait.
        {
           
            var applicationDbContext = _context.Products.Include(p => p.Category); // Eager Loading
            var model = await applicationDbContext.ToListAsync();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                model = await _context.Products.Where(p => p.ProductName.ToLower().Contains(searchTerm)).ToListAsync();
                return PartialView("_ProductList", model);
            }
            else
            {
                return View(model);
            }

            // Return to .cshtml

        }

        [HttpGet]

        public async Task<IActionResult> Search(string searchTerm)
        {
            var applicationDbContext = _context.Products.Include(p => p.Category); // Eager Loading
            var model = await applicationDbContext.ToListAsync();
            if (searchTerm != null)
            {
                model = await _context.Products.Where(p => p.ProductName.Contains(searchTerm)).ToListAsync();
                return PartialView("_ProductList", model);
            }
            else
            {
                return View(model);
            }

             // Return to .cshtml


         
        
        }




        // Get procces for Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products.Include(p => p.Category) // Eager Loading
                    .FirstOrDefaultAsync(m => m.ProductId == id);

            if (products == null)
            {
                return NotFound();
            }
            return View(products);

        }

        // GET: Products/Create 
        public IActionResult Create()
        {
            /*     ViewData["CategoryId"] = new SelectList(_context.Category,  // Created dropdownList to choose.
                                             "CategoryId", "CategoryId");*/
            ViewBag.categoryList = new SelectList(_context.Category, "CategoryId", "CategoryName");
            return View();
        }

        // POST : Product/Create
        // To protect from overposting attacks,
        // enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken] // 
        public async Task<IActionResult>
            Create([Bind("ProductId,ProductName,ProductCode,ProductDescription" +
            ",ProductPrice,CategoryId")]Products products,IFormFile? ImageUpload) // the user fills the necessary fields.
        {

            // Image Upload Process
            if(ImageUpload != null)
            {
                // This row gets type of file like .png , .jpg etc.
                var extension = Path.GetExtension(ImageUpload.FileName);

                // This row creates an unique name for file
                string newName = Guid.NewGuid().ToString() + extension;

                // This row creates the files will be registered.
                string path = Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot/Urunler/" + newName);


                // This Scope provides to create a new file.
                using(var stream = new FileStream(path, FileMode.Create))
                {
                    ImageUpload.CopyTo(stream);
                }
                products.ProductPicture = newName;

            }

            if (!ModelState.IsValid) // Necessary fields is validated.
            {
                _context.Add(products); // After that marked to add database.
                await _context.SaveChangesAsync(); // added to database
                return RedirectToAction(nameof(Index)); // Redirect to product page.
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, // If adding proccess failed,
                                                                       // this method and under this method which is name 'return View(product)'
                                                                       // returned to beginning page (product adding fields)
                "CategoryId", "CategoryId", products.CategoryId);
            return View(products);

        }

        // GET Products/edit/5  ->  (For Examle)
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var products = await _context.Products.FindAsync(id);
            if (products == null) // Checking null or non null 
            {
                return NotFound();
            }
            //ViewData["CategoryId"] = new SelectList(_context.Category, // Dopdownlist is created.
            //    "CategoryId", "CategoryId", products.CategoryId);
            ViewBag.categoryList = new SelectList(_context.Category, "CategoryId", "CategoryName");

            return View(products);
        }

        // POST Procuct/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,ProductCode" +
                                                           ",ProductDescription,ProductPicture,ProductPrice," +
                                                           "CategoryId")] Products products,IFormFile ImageUpload)
        {
            if (ImageUpload != null)
            {
                // This row gets type of file like .png , .jpg etc.
                var extension = Path.GetExtension(ImageUpload.FileName);

                // This row creates an unique name for file
                string newName = Guid.NewGuid().ToString() + extension;

                // This row creates the files will be registered.
                string path = Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot/Urunler/" + newName);


                // This Scope provides to create a new file.
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    ImageUpload.CopyTo(stream);
                }
                products.ProductPicture = newName;

            }


            if (id != products.ProductId)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(products);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductsExists(products.ProductId))
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId",
                                                   "CategoryId", products.CategoryId);
            return View(products);
        }

        // GEt : Product/Delete/5 (for example) // First We view the product and after that
        // deleting.
        public async Task<IActionResult> Delete(int? id)  // This method  provides us to check the product before delete.
        {
            if (id == null)
            {
                return NotFound();
            }

            var producs = await _context.Products.Include(p => p.Category) // Eager Loading.
                                .FirstOrDefaultAsync(m => m.ProductId == id);

            if (producs == null)
            {
                return NotFound();
            }
            return View(producs);
        }


        // POST : Products/Delete/5
        [HttpPost, ActionName("Delete")] // This part provides to 'DeleteConfirmed' method to view as 'Delete' in URL page.
                                         // And when call the delete method, this methot will run.
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var products = await _context.Products.FindAsync(id); // Finded necessary product
            if (products != null)
            {
                _context.Products.Remove(products); // The product deleted.
            }

            // File Deleting Start
            string path = Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot/Urunler/" + products.ProductPicture);
            FileInfo pathFile = new FileInfo(path);
            if (pathFile.Exists)
            {
                System.IO.File.Delete(pathFile.FullName);
                pathFile.Delete();
            }
            // File Deleting End

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}