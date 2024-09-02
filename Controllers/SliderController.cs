using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BiletPortal.Data;
using BiletPortal.Models;

namespace BiletPortal.Controllers
{
    public class SliderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SliderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sliders
        [HttpGet]
        public async Task<IActionResult>Index(string? searchTerm)
        {
            var model = await _context.Slider.ToListAsync();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                model = await _context.Slider.Where(s => s.Header2.ToLower().Contains(searchTerm)).ToListAsync();
                return PartialView("_SliderList",model);
            }
            return View(model); 
        }

        // GET: Sliders/Details/5
        public async Task<IActionResult>
            Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slider = await _context.Slider
            .FirstOrDefaultAsync(m => m.SliderId == id);
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }

        // GET: Sliders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sliders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
            Create([Bind("SliderId,SliderName,Header1,Header2,Context,SliderImage")] Slider slider,IFormFile ImageUpload)
        {

            if (ImageUpload != null)
            {
                // This row gets type of file like .png , .jpg etc.
                var extension = Path.GetExtension(ImageUpload.FileName);

                // This row creates an unique name for file
                string newName = Guid.NewGuid().ToString() + extension;

                // This row creates the files will be registered.
                string path = Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot/SliderImage/" + newName);


                // This Scope provides to create a new file.
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    ImageUpload.CopyTo(stream);
                }
                slider.SliderImage = newName;

            }


            if (ModelState.IsValid)
            {
                _context.Add(slider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(slider);
        }

        // GET: Sliders/Edit/5
        public async Task<IActionResult>
            Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slider = await _context.Slider.FindAsync(id);
            if (slider == null)
            {
                return NotFound();
            }
            return View(slider);
        }

        // POST: Sliders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
            Edit(int id, [Bind("SliderId,SliderName,Header1,Header2,Context,SliderImage")] Slider slider,IFormFile ImageUpload)
        {
            if (ImageUpload != null)
            {
                // This row gets type of file like .png , .jpg etc.
                var extension = Path.GetExtension(ImageUpload.FileName);

                // This row creates an unique name for file
                string newName = Guid.NewGuid().ToString() + extension;

                // This row creates the files will be registered.
                string path = Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot/SliderImage/" + newName);


                // This Scope provides to create a new file.
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    ImageUpload.CopyTo(stream);
                }
                slider.SliderImage = newName;

            }



            if (id != slider.SliderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(slider);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SliderExists(slider.SliderId))
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
            return View(slider);
        }

        // GET: Sliders/Delete/5
        public async Task<IActionResult>
            Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slider = await _context.Slider
            .FirstOrDefaultAsync(m => m.SliderId == id);
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }

        // POST: Sliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
            DeleteConfirmed(int id)
        {

            var slider = await _context.Slider.FindAsync(id);
            if (slider != null)
            {
                _context.Slider.Remove(slider);
            }

            // File Deleting Start
            string path = Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot/SliderImage/" + slider.SliderImage);
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

        private bool SliderExists(int id)
        {
            return _context.Slider.Any(e => e.SliderId == id);
        }
    }
}


/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BiletPortal.Data;
using BiletPortal.Models;

namespace BiletPortal.Controllers
{
    public class SliderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SliderController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET : Slider/Index
        public async Task<IActionResult> Index()
        {
            return View(await _context.Slider.ToListAsync());
        }


        // GET : Slider/Details/5

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slider = await _context.Slider
                .FirstOrDefaultAsync(m => m.SliderId == id);

            if (slider == null)
            {
                return NotFound();
            }
            return View(slider);
        }

        // GET : Slider/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST : Slider/Create
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind(
            "SliderId,SliderName,Header1,Header2,Context,SliderImage")]
             Slider slider, IFormFile? ImageUpload)
        {
            if(ImageUpload != null)
            {
                // This row gets type of file like .png , .jpg etc.
                var extension = Path.GetExtension(ImageUpload.FileName);

                // This row creates an unique name for file
                string newName = Guid.NewGuid().ToString() + extension;

                string path = Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot/SliderImage/" + newName);

                using(var stream = new FileStream(path, FileMode.Create))
                {
                    ImageUpload.CopyTo(stream);

                }
                slider.SliderImage = newName;


            }
            if (!ModelState.IsValid)
            {
                _context.Add(slider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(slider);
        }


        // GET : Slider/Edit/5

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var slider = await _context.Slider.FindAsync(id);

            if (slider == null)
            {
                return NotFound();
            }
            return View(slider);
        }


        // POST : Slider/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id ,[
            Bind("SliderId,SliderName,Header1,Header2," +
            "Context,SliderImage")]Slider slider,IFormFile? ImageUpload)
        {
            if (ImageUpload != null)
            {
                // This row gets type of file like .png , .jpg etc.
                var extension = Path.GetExtension(ImageUpload.FileName);

                // This row creates an unique name for file
                string newName = Guid.NewGuid().ToString() + extension;

                string path = Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot/SliderImage/" + newName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    ImageUpload.CopyTo(stream);

                }
                slider.SliderImage = newName;
            }




            if (id != slider.SliderId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {

                try
                {
                    _context.Update(slider);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SliderExists(slider.SliderId))
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
            return View(slider);
        }

        // GET : Slider/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var slider = await _context.Slider.FirstOrDefaultAsync(m => m.SliderId == id);


            if (slider == null)
            {
                return NotFound();
            }
            return View(slider);

        }

        // POST : Slider/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var slider = await _context.Slider.FindAsync(id);

            if (slider != null)
            {
                _context.Slider.Remove(slider);
            }

            // File Deleting Start
            string path = Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot/SliderImage/" + slider.SliderImage);
            FileInfo pathFile =new FileInfo(path);
            if (pathFile.Exists)
            {
                System.IO.File.Delete(pathFile.FullName);
                pathFile.Delete();  
            }
            // File Deleting End
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SliderExists(int id)
        {
            return _context.Slider.Any(e => e.SliderId == id);
        }
    }
}
*/
