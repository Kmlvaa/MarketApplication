using ConsoleTables;
using MarketApplication.Data.Enum;
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
        public static int AddProduct(Category category, string productName, decimal price, int count)
        {
            if (string.IsNullOrWhiteSpace(productName))
                throw new Exception("Product name can't be empty!");
            if (price < 0)
                throw new Exception("Price can't be less than 0!");
            if (count <= 0)
                throw new Exception("Count can't be less than 0!");

            var product = new Product
            {
                Price = price,
                Name = productName,
                Count = count,
                Categories = category
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
        public static List<Product> UpdateProduct(int id, int count, decimal price, string name, Category category)
        {
            var product = Products.FirstOrDefault(x => x.Id == id)?? throw new Exception($"Product with ID{id} could not found!");
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Name can't be empty!");
            if (count <= 0)
                throw new Exception("Count can not be less than zero!");
            if (price < 0)
                throw new Exception("Price can not be less than zero!");
            product.Price = price;
            product.Name = name;
            product.Count = count;
            product.Categories = category;
            return Products;
        }
        public static List<Product> ShowProductsByCategory(int value)
        {
            var catelogy = Enum.GetName(typeof(Category), value);
            if (catelogy is null) throw new Exception($"Category is invalid!");
            var prd = Products.Where(x => x.Categories.ToString().Contains(catelogy)).ToList();
            return prd;
        }
        public static List<Product> ShowProductsByName(string name)
        {
            var products = Products.FindAll(x => x.Name == name).ToList() ?? throw new Exception($"Products with name {name} does not exist!");
            return products;
        }
        public static List<Product> ShowProductsByPriceRange(decimal minValue, decimal maxValue)
        {
            var products = Products.Where(x => x.Price >= minValue &&  x.Price <= maxValue).ToList() ?? throw new Exception("There is no product in this price range");
            return products;
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
