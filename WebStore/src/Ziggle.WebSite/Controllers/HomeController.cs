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
        private readonly IUserManager userManager;
        private readonly IShoppingCartManager shoppingCartManager;

        public HomeController(ICategoryManager categoryManager, IProductManager productManager, IUserManager userManager,
            IShoppingCartManager shoppingCartManager)
        {
            this.categoryManager = categoryManager;
            this.productManager = productManager;
            this.userManager = userManager;
            this.shoppingCartManager = shoppingCartManager;
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LoginModel loginModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.LogIn(loginModel.UserName, loginModel.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "User name and password do not match.");
                }
                else
                {
                    Session["User"] = new Ziggle.WebSite.Models.UserModel { Id = user.Id, Name = user.Name };

                    System.Web.Security.FormsAuthentication.SetAuthCookie(loginModel.UserName, false);

                    return Redirect(returnUrl ?? "~/");
                    //return Redirect(returnUrl == null ? "~/" : returnUrl);
                }
            }

            return View(loginModel);
        }

        public ActionResult LogOff()
        {
            Session["User"] = null;
            System.Web.Security.FormsAuthentication.SignOut();

            return Redirect("~/");
        }

        [Authorize]
        public ActionResult AddToCart(int id)
        {
            var user = (Ziggle.WebSite.Models.UserModel)Session["User"];

            var item = shoppingCartManager.Add(user.Id, id, 1);

            var items = shoppingCartManager.GetAll(user.Id)
                .Select(t => new Ziggle.WebSite.Models.ShoppingCartItem {
                    UserId = t.UserId, 
                    ProductId = t.ProductId,
                    Quantity = t.Quantity })
                .ToArray();

            return View(items);
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