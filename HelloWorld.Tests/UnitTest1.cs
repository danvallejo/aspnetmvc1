using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using HelloWorld.Models;
using System.Collections.Generic;
using HelloWorld.Controllers;
using System.Linq;
using Moq;
//using HelloWorld;

namespace HelloWorld.Tests
{
    public class FakeProductRepository : IProductRepository
    {
        public IEnumerable<Product> Products
        {
            get
            {
                var items = new[]
                {
                    new Product{ Name="Baseball", Price = 11},
                    new Product{ Name="Football", Price = 8},
                    new Product{ Name="Tennis ball", Price = 13} ,
                    new Product{ Name="Golf ball", Price = 3},
                    new Product{ Name="ping pong ball", Price = 12},
                };
                return items;
            }
        }
    }

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethodWithMoq()
        {
            var mockProductRepository = new Mock<IProductRepository>();

            mockProductRepository
                .SetupGet(t => t.Products)
                .Returns(
                    () =>
                    {
                        return new Product[]{
                            new Product{ Name="Baseball", Price = 11},
                            new Product{ Name="Football", Price = 8},
                            new Product{ Name="Tennis ball", Price = 13} ,
                            new Product{ Name="Golf ball", Price=3},
                            new Product{ Name="ping pong ball", Price = 12},
                        };
                    }
                 );

            // Arrange
            var controller = new HomeController(mockProductRepository.Object);

            // Act
            var result = controller.Products();

            // Assert
            var products = (Product[])((System.Web.Mvc.ViewResultBase)(result)).Model;
            Assert.AreEqual(5, products.Length, "Length is invalid");

            Assert.AreEqual(33, products.Where(t => t.Price > 10).Count(), "We should have 3 products > $10");
            Assert.AreEqual(22, products.Where(t => t.Price < 10).Count(), "We should have 2 products < $10");
        }

        [TestMethod]
        public void TestMethodWithFakeClass()
        {
            // Arrange
            var controller = new HomeController(new FakeProductRepository());

            // Act
            var result = controller.Products();

            // Assert
            var products = (Product[])((System.Web.Mvc.ViewResultBase)(result)).Model;
            Assert.AreEqual(5, products.Length);
        }
    }
}
