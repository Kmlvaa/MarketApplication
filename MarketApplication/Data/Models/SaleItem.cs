using MarketApplication.Data.Common;

namespace MarketApplication.Data.Models
{
    public class SaleItem : BaseModel
    {
        private static int _id = 0;
        public SaleItem()
        {
            Id = _id;
            _id++;
        }
        public int Count { get; set; }
        public Product? SaleProduct { get; set; }
    }
}
