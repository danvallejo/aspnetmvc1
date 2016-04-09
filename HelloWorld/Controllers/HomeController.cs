using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using HelloWorld.Models;
using System.Web.UI;

namespace HelloWorld.Controllers
{
    //[Logging]
    //[AuthorizeIPAddress]
    public class HomeController : Controller
    {
        private IProductRepository productRepository;
        private IUserRepository userRepository;

        public HomeController(IProductRepository productRepository, IUserRepository userRepository)
        {
            this.productRepository = productRepository;
            this.userRepository = userRepository;
        }

        [Authorize]
        [IsAdministrator]
        public ActionResult Notes()
        {
            return View();
        }

        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = userRepository.LogIn(model.UserName, model.Password);
                if (user != null)
                {
                    Session["User"] = user;
                    System.Web.Security.FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    return Redirect(returnUrl);
                }

                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            }

            return View(model);
        }

        public ActionResult LogOff()
        {
            Session["User"] = null;
            System.Web.Security.FormsAuthentication.SignOut();
            return Redirect("~/");
        }

        public ActionResult SetCookie()
        {
            var cookie = new HttpCookie("MyCookie");
            cookie.Expires = DateTime.Now.AddMinutes(1);
            cookie.Value = "myUserName";
            HttpContext.Response.Cookies.Add(cookie);
            return View(cookie);
        }

        public ActionResult GetCookies()
        {
            return View(HttpContext.Request.Cookies["MyCookie"]);
        }

        public PartialViewResult IncrementCount()
        {
            int count = 0;
            if (Session["MyCount"] != null)
            {
                count = (int)Session["MyCount"];
                count++;
            }

            Session["MyCount"] = count;
            return new PartialViewResult();
        }

        public ActionResult Product()
        {
            return View(productRepository.Products.First());
        }

        public ActionResult Products()
        {
            ViewBag.MyCount = 1;
            return View(productRepository.Products);
        }

        //
        // GET: /Home/
        //[OutputCache(Duration = 10, Location = OutputCacheLocation.Any, VaryByParam = "none")]
        public ActionResult Index()
        {
            //int x = 1;
            //x = x / (x - 1);

            return View();
        }

        [HttpGet]
        public ActionResult RsvpForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RsvpForm(GuestResponse guestResponse)
        {
            if (ModelState.IsValid)
            {
                return View("Thanks", guestResponse);
            }
            else
            {
                return View();
            }
        }
    }
}