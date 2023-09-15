using ConsoleTables;
using MarketApplication.Data.Models;
using MarketApplication.Servers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MarketApplication.Servers.Concrete
{
    public class MarketService //: IMarket
    {
        public static List<Product> Products = new();
        public static List<Sale> Sales = new();

        public static List<Product> GetProduct()
        {
            return Products;
        }
        public static int AddProduct(string productName, decimal price, int count)
        {
            if (string.IsNullOrWhiteSpace(productName))
                throw new Exception("Product name can't be empty!");

            if (price < 0)
                throw new Exception("Price can't be less than 0!");

            var product = new Product
            {
                Price = price,
                Name = productName,
                Count = count
            };

            Products.Add(product);

            return product.Count;
        }
        public static int DeleteProduct(int id)
        {
            if (id < 0) throw new Exception("ID can not be less than 0!");
            var product = Products.FirstOrDefault(x => x.Id == id) ?? throw new Exception($"Product with ID:{id} was not found!");
            Products.Remove(product);
            return product.Count;
        }
       
        public int AddSales(int id)
        {
            if (id < 0)
                throw new Exception("Price can't be less than 0!");
            var sales = Sales.FirstOrDefault(x => x.Id == id) ?? throw new Exception($"Doctor with ID:{id} was not found!");
            var sale = new Sale
            {
               
            };
            Sales.Add(sale);

            return Sales.Count;
        }
    }
}
