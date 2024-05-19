using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        // Initialize the ApplicationDbContext to get data from the database
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {

            IEnumerable<Category> objCategorylist = _db.Categories; // OR var objCategorylist = _db.Categories.ToList();
            return View(objCategorylist);
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "Name and Display Order cannot be the same");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges(); // save to the database
                TempData["Success"] = "Category successfully created";
                return RedirectToAction("Index");

            }
            return View(obj);
        }

        // GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);
            var categoryFromDbUsingFirstOrDefault = _db.Categories.FirstOrDefault(u => u.Id == id);
            var categoryFromDbUsingSingOrDefault = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "Name and Display Order cannot be the same");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges(); // save to the database
                TempData["Success"] = "Category successfully updated";
                return RedirectToAction("Index");

            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);
            //var categoryFromDbUsingFirstOrDefault = _db.Categories.FirstOrDefault(u => u.Id == id);
            //var categoryFromDbUsingSingOrDefault = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        // POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return BadRequest();
            }


            _db.Categories.Remove(obj);
            _db.SaveChanges(); // save to the database
            TempData["Success"] = "Category successfully deleted";
            return RedirectToAction("Index");
        }
    }
}
