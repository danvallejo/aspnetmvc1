using System.Linq;

namespace Ziggle.Repository
{
    public interface ICategoryRepository
    {
        CategoryModel[] Categories { get; }
        CategoryModel Category(int categoryId);
    }

    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CategoryRepository : ICategoryRepository
    {
        public CategoryModel[] Categories
        {
            get
            {
                return ProductDatabase.Instance.Categories
                    .Select(t => new CategoryModel { Id = t.CategoryId, Name = t.CategoryName })
                    .ToArray();
            }
        }

        public CategoryModel Category(int categoryId)
        {
            var category = ProductDatabase.Instance.Categories
                .Where(t => t.CategoryId == categoryId)
                .Select(t => new CategoryModel { Id = t.CategoryId, Name = t.CategoryName })
                .First();
            return category;
        }
    }
}