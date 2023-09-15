using MarketApplication.Data.Common;

namespace MarketApplication.Data.Models
{
    public class SaleItem : BaseModel
    {
        public int Count { get; set; }
        public List<Product> Product { get; set; } = new();
    }
}
