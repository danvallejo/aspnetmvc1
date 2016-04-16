using Ziggle.ProductDatabase;

namespace Ziggle.Repository
{
    public class ProductDatabase
    {
        private static readonly ProductDbEntities entities;

        static ProductDatabase()
        {
            entities = new ProductDbEntities();
            entities.Database.Connection.Open();
        }

        public static ProductDbEntities Instance
        {
            get
            {
                return entities;
            }
        }
    }
}