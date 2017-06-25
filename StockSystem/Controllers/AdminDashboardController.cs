using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StockSystem.Controllers
{
    public class AdminDashboardController : Controller
    {
        // GET: AdminDashboard
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}