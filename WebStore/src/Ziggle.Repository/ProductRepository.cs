using System.Linq;

namespace Ziggle.Repository
{
    public interface IProductRepository
    {
        ProductModel[] ForCategory(int categoryId);
    }

    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public class ProductRepository : IProductRepository
    {
        public ProductModel[] ForCategory(int categoryId)
        {
            return ProductDatabase.Instance.Categories
                                  .First(t => t.CategoryId == categoryId)
                                  .Products
                                  .Select(t =>
                                        new ProductModel
                                        {
                                            Id = t.ProductId,
                                            Name = t.ProductName,
                                            Price = t.ProductPrice,
                                            Quantity = t.ProductQuantity
                                        })
                                  .ToArray();
        }
    }
}