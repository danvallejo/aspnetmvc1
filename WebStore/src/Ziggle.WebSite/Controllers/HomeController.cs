using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Ziggle.Business;
using Ziggle.WebSite.Models;

namespace Ziggle.WebSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryManager categoryManager;

        public HomeController(ICategoryManager categoryManager)
        {
            this.categoryManager = categoryManager;
        }

        public ActionResult Index()
        {
            var categories = categoryManager.Categories
                .Select(t => new Ziggle.WebSite.Models.CategoryModel(t.Id, t.Name))
                .ToArray();

            var model = new IndexModel { Categories = categories };

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}