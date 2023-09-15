using MarketApplication.Data.Models;
using System;

namespace MarketApplication.Servers.Abstract
{
    public interface IMarket
    {
        public abstract int AddProduct(string productName, decimal price, int count);
        public abstract int DeleteProduct(int id);
        public abstract List<Product> UpdateProducts();
        public abstract List<Product> GetProducts();
        public abstract List<Product> GetProductByName();
        public abstract List<Product> GetProductByCategory();
        public  abstract List<Product> GetProductPriceRange();
        

        public abstract int AddSale();
        public abstract int DeleteSale(int id);
        public abstract List<Product> UpdateSales();
        public abstract List<Sale> GetSales();
        public abstract List<Sale> GetSaleById();
        public abstract List<Sale> GetSaleByDate();
        public abstract List<Sale> GetSaleByDateRange();
        public abstract List<Sale> GetSalesByPriceRange();

    }
}
