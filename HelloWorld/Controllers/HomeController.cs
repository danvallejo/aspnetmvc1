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

        public HomeController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
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