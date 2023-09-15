using MarketApplication.Data.Common;
using MarketApplication.Data.Enum;

namespace MarketApplication.Data.Models
{
    public class Product : BaseModel
    {
        private static int _id = 0;
        public Product()
        {
            Id = _id;
            _id++;
        }

        public string Name { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public Category Categories { get; set; }
    }
}
