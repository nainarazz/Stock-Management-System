using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StockSystem.Models;
using StockSystem.ViewModels;
using System.Globalization;

namespace StockSystem.Controllers
{
    public class CategoryController : Controller
    {
        private ApplicationDbContext _context;
        
        public CategoryController ()
	    {
            _context = new ApplicationDbContext();

	    }
        
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }                

        // GET: Category
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            var viewModel = new AddCategoryViewModel();

            return PartialView("_Add", viewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(Category category)
        {            
            if (!ModelState.IsValid)
            {
                var viewModel = new AddCategoryViewModel(category);
                return PartialView("_Add", viewModel);
            }
            
            category.CategoryName = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(category.CategoryName.ToLower());

            _context.Category.Add(category);
            _context.SaveChanges();

            string url = Url.Action("Index", "Category");
            return Json(new { success = true, url = url });
        }

        public ActionResult Edit(int id)
        {
            var category = _context.Category.SingleOrDefault(c => c.Id == id);
            AddCategoryViewModel.tempCategoryName = category.CategoryName;

            if (category == null)
                return HttpNotFound();
           
            return PartialView("_Edit", category);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            var categoryNameExistsInDb = _context.Category.Any(c => c.CategoryName == category.CategoryName);

            if (AddCategoryViewModel.tempCategoryName != category.CategoryName && categoryNameExistsInDb)
                ModelState.AddModelError("CategoryName", "Category name already exists.");

            if (!ModelState.IsValid)
            {                
                return PartialView("_Edit", category);
            }
           
            var categoryInDb = _context.Category.Single(c => c.Id == category.Id);

            categoryInDb.CategoryName = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(category.CategoryName.ToLower());
            categoryInDb.CategoryDescription = category.CategoryDescription; 

            _context.SaveChanges();

            string url = Url.Action("Index", "Category");
            return Json(new { success = true, url = url });

        }

        public ActionResult Delete(int id)
        {
            var categoryInDb = _context.Category.SingleOrDefault(c => c.Id == id);

            if (categoryInDb == null)
                return HttpNotFound();

            _context.Category.Remove(categoryInDb);
            _context.SaveChanges();

            return RedirectToAction("Index", "Category");

        }

        //code adapted from a post by Asma Khalid
        //http://www.c-sharpcorner.com/article/Asp-Net-mvc5-datatables-plugin-server-side-integration/ 
        public ActionResult GetCategory()
        {
            JsonResult result = new JsonResult();

            var draw = Request.Params["draw"];
            var search = Request.Params["search[value]"];
            var order = Request.Params["order[0][column]"];
            var orderDir = Request.Params["order[0][dir]"]; 
            var startRec = Convert.ToInt32(Request.Params["start"]);
            var pageSize = Convert.ToInt32(Request.Params["length"]);

            var category = _context.Category.ToList();
            var recordsTotal = category.Count;            
            
            if (!string.IsNullOrEmpty(search))
            {
                // Apply search   
                category = category.Where(c => c.CategoryName.ToLower().Contains(search.ToLower())).ToList();
            }

            // Sorting.   
            category = SortByColumnWithOrder(order,orderDir, category);

            // Filter record count.             
            var recordsFiltered = category.Count;

            // Apply pagination.   
            category = category.Skip(startRec).Take(pageSize).ToList();

            return result = Json(new
            {
                draw = Convert.ToInt32(draw),
                recordsTotal = recordsTotal,
                recordsFiltered = recordsFiltered,
                category = category
            }, JsonRequestBehavior.AllowGet);
        }

        private List<Category> SortByColumnWithOrder(string order, string orderDir, List<Category> category)
        {

            var listCategory = new List<Category>();
            //orderDir = "DESC";

            // Sorting  

            switch (order)
            {
                case "0":
                    listCategory = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? category.OrderByDescending(c => c.CategoryName).ToList() : category.OrderBy(c => c.CategoryName).ToList();
                    break;
                case "1":
                    listCategory = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? category.OrderByDescending(c => c.CategoryName).ToList() : category.OrderBy(c => c.CategoryName).ToList();
                    break;
                case "2":
                    listCategory = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? category.OrderByDescending(c => c.CategoryName).ToList() : category.OrderBy(c => c.CategoryName).ToList();
                    break;
                default:
                    listCategory = category;
                    break;
            }
 
            return listCategory;

        }

    }
}