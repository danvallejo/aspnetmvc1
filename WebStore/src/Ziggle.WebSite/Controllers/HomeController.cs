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
        private readonly IProductManager productManager;

        public HomeController(ICategoryManager categoryManager, IProductManager productManager)
        {
            this.categoryManager = categoryManager;
            this.productManager = productManager;
        }

        public ActionResult Category(int id)
        {
            var category = categoryManager.Category(id);
            var products = productManager
                                .ForCategory(id)
                                .Select(t =>
                                    new Ziggle.WebSite.Models.ProductModel
                                    {
                                        Id = t.Id,
                                        Name = t.Name,
                                        Price = t.Price,
                                        Quantity = t.Quantity
                                    }).ToArray();

            var model = new CategoryViewModel
            {
                Category = new Ziggle.WebSite.Models.CategoryModel(category.Id, category.Name),
                Products = products
            };

            return View(model);
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