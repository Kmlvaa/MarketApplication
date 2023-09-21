using MarketApplication.Data.Enum;
using MarketApplication.Data.Models;
using System;

namespace MarketApplication.Servers.Abstract
{
    public interface IMarket
    {
        public abstract int AddProduct(Category category, string productName, decimal price, int count);
        public abstract int DeleteProduct(int id);
        public abstract List<Product> UpdateProduct(int id, int count, decimal price, string name, Category category);
        public abstract List<Product> GetProducts();
        public abstract List<Product> GetProductsByName(string name);
        public abstract List<Product> GetProductsByCategory(int value);
        public abstract List<Product> GetProductsByPriceRange(decimal minValue, decimal maxValue);

        public abstract int AddSale(int num);
        public abstract List<Sale> DeleteSale(int id);
        public abstract List<Sale> SaleWithDraw(int saleId);
        public abstract List<Sale> GetSales();
        public abstract List<Sale> GetSaleItems();
        public abstract List<Sale> GetSaleById(int id);
        public abstract List<SaleItem> GetSaleItemsBySaleId(int id);
        public abstract List<Sale> GetSalesByDate(DateTime date);
        public abstract List<Sale> GetSalesByDateRange(DateTime min, DateTime max);
        public abstract List<Sale> GetSalesByPriceRange(decimal min, decimal max);

    }
}
