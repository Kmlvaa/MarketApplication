using MarketApplication.Data.Enum;
using MarketApplication.Data.Models;

namespace MarketApplication.Servers.Concrete
{
    public class MarketService //: IMarket
    {
        public static List<Product> Products = new();
        public static List<SaleItem> SaleItems = new();
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
        public static List<Product> UpdateProduct(int id, int count, decimal price, string name)
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
            return Products;
        }
        public static List<Product> ShowProductsByCategory(int value)
        {
            var category = Enum.GetName(typeof(Category), value);
            if (category is null) throw new Exception($"Category is invalid!");
            var prd = Products.Where(x => x.Categories.ToString().Contains(category)).ToList();
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

        public static int AddSale(int count)
        {
            var sale = new Sale();
            Console.Write("Enter number of saleItems: ");
            int num = int.Parse(Console.ReadLine()!);
            int id;
            var product = new Product { };
            ;
            for (int i = 0; i < num; i++)
            {
                Console.Write("Enter sale's id: ");
                id = int.Parse(Console.ReadLine()!);
                product = Products.FirstOrDefault(x => x.Id == id) ?? throw new Exception($"Product with ID:{id} was not found!");
                var saleItem = new SaleItem { 
                   Count = count,
                   SaleProduct = product
                };
                sale.Items.Add(saleItem);
            }
                var price = sale.Items.Select(x => x.SaleProduct.Price);
                

                sale.Date = DateTime.Now;
            sale.Amount = 0;
                if (count > 0)
                {
                    throw new Exception("The number of products cannot exceed the number in stock!");
                }

            return Sales.Count;
        }
        public static List<Sale> ShowSales()
        {
            return Sales;
        }
        public static List<Sale> DeleteSale(int id)
        {
            var product = SaleItems.Where(x => x.Id == id).Select(n => n) ?? throw new Exception($"Product with ID:{id} was not found!");
            SaleItems.Remove((SaleItem)product);
            return Sales;
        }
    }
}
