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
        public static List<Product> GetProductsByCategory(int value)
        {
            var category = Enum.GetName(typeof(Category), value);
            if (category is null) throw new Exception($"Category is invalid!");
            var prd = Products.Where(x => x.Categories.ToString().Contains(category)).ToList();
            return prd;
        }
        public static List<Product> GetProductsByName(string name)
        {
            var products = Products.FindAll(x => x.Name == name).ToList() ?? throw new Exception($"Products with name {name} does not exist!");
            return products;
        }
        public static List<Product> GetProductsByPriceRange(decimal minValue, decimal maxValue)
        {
            var products = Products.Where(x => x.Price >= minValue &&  x.Price <= maxValue).ToList() ?? throw new Exception("There is no product in this price range");
            return products;
        }

        public static int AddSale(int num)
        {
            var sale = new Sale();
            var product = new Product();
            int count;
            decimal price = 0,amount = 0;
            for (int i = 0; i < num; i++)
            {
                Console.Write("Enter sale's id: ");
                int id = int.Parse(Console.ReadLine()!);

                Console.Write("Enter sale's count: ");
                count = int.Parse(Console.ReadLine()!);

                product = Products.FirstOrDefault(x => x.Id == id) ?? throw new Exception($"Product with ID:{id} was not found!");
                var saleItem = new SaleItem { 
                   Count = count,
                   SaleProduct = product
                };
                sale.Items.Add(saleItem);
                price = count * product.Price;
                amount += price;

                if (count > product.Count)
                {
                    throw new Exception("The number of products cannot exceed the number in stock!");
                }
                product.Count -= count;
                SaleItems.Add(saleItem);
            }
            sale.Date = DateTime.Now;
            sale.Amount = amount;
            Sales.Add(sale);

            return sale.Id;
        }
        public static List<Sale> GetSales()
        {
            SaleItems.ForEach(SaleItems.Add);
            return Sales;
        }
        public static List<Sale> DeleteSale(int id)
        {
            var product = Sales.FirstOrDefault(x => x.Id == id) ?? throw new Exception($"Product with ID:{id} was not found!");
            //var x = SaleItems.Remove(product);
            Sales.Remove(product);
            return Sales;
        }
        public static List<Sale> SaleWithDraw(int num)
        {
            Console.Write("Enter sale's ID:");
            int saleId = int.Parse(Console.ReadLine()!);

            for (int i = 0; i < num; i++)
            {
                Console.Write("Enter saleItem's ID:");
                int saleItemId = int.Parse(Console.ReadLine()!);

                Console.Write("Enter saleItem's count:");
                int count = int.Parse(Console.ReadLine()!);
                
                var sale = Sales.FirstOrDefault(x => x.Id == saleId) ?? throw new Exception($"Product with ID:{saleId} was not found!");
                var saleItem = SaleItems.FirstOrDefault(x => x.Id == saleItemId) ?? throw new Exception($"Product with ID:{saleItemId} was not found!");
                saleItem.Count -= count;
                sale.Amount -= count * saleItem.SaleProduct!.Price; 
                saleItem.SaleProduct!.Count += count;
                if(count > saleItem.Count)
                {
                    throw new Exception("Count can not be less than saleItem's count!");
                }
            }
            return Sales;
        }
    }
}
