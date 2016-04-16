using System.Linq;

namespace Ziggle.Repository
{
    public interface IShoppingCartRepository
    {
        ShoppingCartModel Add(int userId, int productId, int quantity);
        bool Remove(int userId, int productId);
        ShoppingCartModel[] GetAll(int userId);
    }

    public class ShoppingCartModel
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class ShoppingCartRepository : IShoppingCartRepository
    {
        public ShoppingCartModel Add(int userId, int productId, int quantity)
        {
            var item = ProductDatabase.Instance.ShoppingCartItems.Add(
                new Ziggle.ProductDatabase.ShoppingCartItem { ProductId = productId, UserId = userId, Quantity = quantity });

            ProductDatabase.Instance.SaveChanges();

            return new ShoppingCartModel { UserId = item.UserId, ProductId = item.ProductId, Quantity = item.Quantity };
        }

        public ShoppingCartModel[] GetAll(int userId)
        {
            var items = ProductDatabase.Instance.ShoppingCartItems
                .Where(t => t.UserId == userId)
                .Select(t => new ShoppingCartModel { 
                    UserId = t.UserId,
                    ProductId = t.ProductId,
                    Quantity = t.Quantity })
                .ToArray();
            return items;
        }

        public bool Remove(int userId, int productId)
        {
            var items = ProductDatabase.Instance.ShoppingCartItems.Where(t => t.UserId == userId && t.ProductId == productId);

            if (items.Count() == 0)
            {
                return false;
            }

            ProductDatabase.Instance.ShoppingCartItems.Remove(items.First());

            ProductDatabase.Instance.SaveChanges();

            return true;
        }
    }
}