using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StockSystem.Models;
using StockSystem.ViewModels;
using System.ComponentModel.DataAnnotations;


namespace StockSystem.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext _context;
        
        public ProductController()
        {
            _context = new ApplicationDbContext();
        }
        
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        
        // GET: Products
        [Authorize]
        public ActionResult Index()
        {            
            return View();
        }

        public ActionResult CheckReferenceId()
        {
            var ReferenceId = Request.Params["ReferenceId"];
            var productInDb = _context.Product.Any(p => p.ReferenceId == ReferenceId);

            return Json(!productInDb, JsonRequestBehavior.AllowGet);

        }  
     
        public ActionResult New()
        {
            var category = _context.Category.ToList();            

            var viewModel = new AddProductViewModel()
            {             
                Category = category
            };

            return PartialView("_Add", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]       
        public ActionResult New(Product product)
        {
            var viewModel = new AddProductViewModel(product);

            if (!ModelState.IsValid)
            {
                viewModel.Category = _context.Category.ToList();
                return PartialView("_Add", viewModel);
            }            

                _context.Product.Add(product);
                _context.SaveChanges();

                string url = Url.Action("Index", "AdminDashboard");
                return Json(new { success = true, url = url });                    
        }

        public ActionResult Edit(string Id)
        {
            var products = _context.Product.SingleOrDefault(p => p.ReferenceId == Id);

            if (products == null)
                return HttpNotFound();

            var viewModel = new EditProductViewModel(products)
            {
                Category = _context.Category.ToList()
            };

            return PartialView("_Edit", viewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            var viewModel = new EditProductViewModel(product);            

            if (!ModelState.IsValid)
            {
                viewModel.Category = _context.Category.ToList();
                return PartialView("_Edit", viewModel);
            }

            var productInDb = _context.Product.Single(p => p.ReferenceId == product.ReferenceId);

            productInDb.ReferenceId = product.ReferenceId;
            productInDb.ItemName = product.ItemName;
            productInDb.CategoryId = product.CategoryId;
            productInDb.ItemUnit = product.ItemUnit;
            productInDb.UnitPrice = product.UnitPrice;
            productInDb.StockLevel = product.StockLevel;
            productInDb.ShelfLocation = product.ShelfLocation;
            productInDb.MinimumLevel = product.MinimumLevel;

            _context.SaveChanges();
           
            string url = Url.Action("Index", "AdminDashboard");
            return Json(new { success = true, url = url });
            
        }      
        
        //Delete        
        public ActionResult Delete(string referenceID)
        {
            var productsInDb = _context.Product.SingleOrDefault(m => m.ReferenceId == referenceID);

            if (productsInDb == null)
                return HttpNotFound();

            _context.Product.Remove(productsInDb);
            _context.SaveChanges();

            return RedirectToAction("Index", "Product");
            
        }

        //code adapted from a post by Asma Khalid
        //http://www.c-sharpcorner.com/article/Asp-Net-mvc5-datatables-plugin-server-side-integration/        
        public ActionResult GetProducts()
        {
            JsonResult result = new JsonResult();

            var draw = Request.Params["draw"];
            var search = Request.Params["search[value]"];
            var order = Request.Params["order[0][column]"];
            var orderDir = Request.Params["order[0][dir]"]; 
            var startRec = Convert.ToInt32(Request.Params["start"]);
            var pageSize = Convert.ToInt32(Request.Params["length"]);            
            
            var products = _context.Product.Include(p => p.Category).ToList();
            var recordsTotal = products.Count;            
            
            if (!string.IsNullOrEmpty(search))
            {
                // Apply search   
                products = products.Where(m => m.ItemName.ToLower().Contains(search.ToLower()) ||
                    m.Category.CategoryName.ToLower().Contains(search.ToLower()) ||
                    m.ReferenceId.ToString().ToLower().Contains(search.ToLower())).ToList();
            }

            // Sorting.   
            products = SortByColumnWithOrder(order, orderDir, products);

            // Filter record count.             
            var recordsFiltered = products.Count;

            // Apply pagination.   
            products = products.Skip(startRec).Take(pageSize).ToList();

            return result = Json(new
            {
                draw = Convert.ToInt32(draw),
                recordsTotal = recordsTotal,
                recordsFiltered = recordsFiltered,
                product = products
            }, JsonRequestBehavior.AllowGet);
        }

        private List<Product> SortByColumnWithOrder(string order, string orderDir, List<Product> product)
        {

            var listProduct = new List<Product>();
            //orderDir = "DESC";

            // Sorting   
            switch (order)
            {
                case "0":
                    listProduct = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? product.OrderByDescending(p => p.ReferenceId).ToList() : product.OrderBy(p => p.ReferenceId).ToList();
                    break;
                case "1":
                    listProduct = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? product.OrderByDescending(p => p.Category.CategoryName).ToList() : product.OrderBy(p => p.Category.CategoryName).ToList();
                    break;
                case "2":
                    listProduct = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? product.OrderByDescending(p => p.ItemName).ToList() : product.OrderBy(p => p.ItemName).ToList();
                    break;
                case "3":
                    listProduct = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? product.OrderByDescending(p => p.ItemUnit).ToList() : product.OrderBy(p => p.ItemUnit).ToList();
                    break;
                case "4":
                    listProduct = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? product.OrderByDescending(p => p.StockLevel).ToList() : product.OrderBy(p => p.StockLevel).ToList();
                    break;
                case "5":
                    listProduct = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? product.OrderByDescending(p => p.UnitPrice).ToList() : product.OrderBy(p => p.UnitPrice).ToList();
                    break;
                case "6":
                    listProduct = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? product.OrderByDescending(p => p.ShelfLocation).ToList() : product.OrderBy(p => p.ShelfLocation).ToList();
                    break;
                default:
                    listProduct = product;
                    break;
            }

            return listProduct;

        }


    }
}