using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using HelloWorld.Models;
using System.Web.Caching;

namespace HelloWorld
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
    }

    public class ProductRepository : IProductRepository
    {
        public IEnumerable<Product> Products
        {
            get
            {
                if (HttpContext.Current.Cache["MyProducts"] == null)
                {
                    var items = new[]
                    {
                        new Product{ Name = "Baseball", Price = 1},
                        new Product{ Name="Football", Price =2 },
                        new Product{ Name="Tennis ball", Price=11} ,
                        new Product{ Name="Golf ball", Price = 12},
                    };

                    HttpContext.Current.Cache.Insert("MyProducts",
                        items, null,
                        DateTime.Now.AddSeconds(10),
                        Cache.NoSlidingExpiration);
                }

                return (IEnumerable<Product>)HttpContext.Current.Cache["MyProducts"];
            }
        }
    }
}