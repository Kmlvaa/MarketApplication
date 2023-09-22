using MarketApplication.Data.Enum;
using MarketApplication.Data.Models;
using MarketApplication.Servers.Abstract;

namespace MarketApplication.Servers.Concrete
{
    public class MarketService : IMarket
    {
        public static List<Product> Products = new();
        public static List<SaleItem> SaleItems = new();
        public static List<Sale> Sales = new();

        public List<Product> GetProducts()
        {
            return Products;
        }
        public int AddProduct(Category category, string productName, decimal price, int count)
        {
            //check if given string is null or white space
            if (string.IsNullOrWhiteSpace(productName))
                throw new Exception("Product name can't be empty!");

            //check if given name is number
            if (productName.All(char.IsDigit))
                throw new Exception("Product name can not be number!");

            //check if given number is matching with the category within eNum(Category)
            if (!Enum.IsDefined(typeof(Category), category))
                throw new Exception("Category is not found!");

            //check if price and count is less than zero
            if (price < 0)
                throw new Exception("Price can't be less than 0!");
            if (count <= 0)
                throw new Exception("Count can't be less than 0!");

            //create new instance of product class and push the values 
            var product = new Product
            {
                Price = price,
                Name = productName,
                Count = count,
                Categories = category
            };
            //Adds object to Products list
            Products.Add(product);

            return product.Count;
        }

        public int DeleteProduct(int id)
        { 
            //Filter products by id and Checks id to detect exception
            var product = Products.FirstOrDefault(x => x.Id == id) ?? throw new Exception($"Product with ID:{id} was not found!");
            //Removes product with given id from list
            Products.Remove(product);

            return product.Count;
        }

        public List<Product> UpdateProduct(int id, int count, decimal price, string name, Category category)
        {
            //Filter products by id and Checks id to detect exception
            var product = Products.FirstOrDefault(x => x.Id == id)?? throw new Exception($"Product with ID{id} could not found!");
            
            //Checks data to detect exceptions
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Name can not be empty!");
            if (!Enum.IsDefined(typeof(Category), category))
                throw new Exception("Category is not found!");
            if (count <= 0)
                throw new Exception("Count can not be less than zero!");
            if (price < 0)
                throw new Exception("Price can not be less than zero!");
            //Update product's datas 
            product.Price = price;
            product.Name = name;
            product.Count = count;
            product.Categories = category;

            return Products;
        }

        public List<Product> GetProductsByCategory(int value)
        {
            //Checks if given numbers are matching with category values
            var category = Enum.GetName(typeof(Category), value);
            if (category is null) throw new Exception($"Category is invalid!");

            //Filter products by category and Checks to detect exception
            var prd = Products.Where(x => x.Categories.ToString().Contains(category)).ToList();

            return prd;
        }

        public List<Product> GetProductsByName(string name)
        {
            //Filter products by name and Checks to detect exception
            var products = Products.FindAll(x => x.Name == name).ToList() ?? throw new Exception($"Products with name {name} does not exist!");
            
            return products;
        }

        public List<Product> GetProductsByPriceRange(decimal minValue, decimal maxValue)
        {
            //Checks if min value is bigger than max value
            if(maxValue < minValue)
                throw new Exception("Min value can not be less than max value!");

            //Filter products within price range and Checks to detect exception
            var products = Products.Where(x => x.Price >= minValue &&  x.Price <= maxValue).ToList() ?? throw new Exception("No matching products found");
            
            return products;
        }

        public int AddSale(int num)
        {
            //Creates new Sale and Product objects
            var sale = new Sale();
            var product = new Product();
            int count = 0;
            //Checks whether the entered quantity is available in stock or not
            if (num > Products.Count)
            {
                throw new Exception("There is not enough product in stock!");
            }
            if(num == 0)
            {
                throw new Exception("End!");
            }
            decimal price = 0,amount = 0;
            for (int i = 0; i < num; i++)
            {
                Console.Write("Enter product's id: ");
                int id = int.Parse(Console.ReadLine()!);

                //Filter list by id and detect exception
                product = Products.FirstOrDefault(x => x.Id == id) ?? throw new Exception($"Product with ID:{id} was not found!");
                
                Console.Write("Enter product's count: ");
                count = int.Parse(Console.ReadLine()!);
                if (count == 0) throw new Exception("Could not add 0 product1");
                if (count > product.Count) throw new Exception("The number of products cannot exceed the number in stock!");
                
                //Sets SaleItem object's properties
                var saleItem = new SaleItem { 
                   Count = count,
                   SaleProduct = product
                };
                sale.Items.Add(saleItem);

                price = count * product.Price;
                amount += price;

                //Reduces the number of products in stock
                product.Count -= count;
                SaleItems.Add(saleItem);
            }
            
            //Sets sale objects properties
            sale.Amount = amount;
            Sales.Add(sale);

            return sale.Id;
        }

        public List<Sale> GetSales()
        {
            return Sales;
        }

        //THIS METHOD IS FOR GETTING SALEITEMS FROM SALE
        public List<Sale> GetSaleItems()
        {
            var sale = Sales.Select(x => x.Items.FirstOrDefault());
            return (List<Sale>)sale;
        }

        public List<Sale> DeleteSale(int id)
        {
            //Filter sales by id and Check if there is no sale 
            var product = Sales.FirstOrDefault(x => x.Id == id) ?? throw new Exception($"Sale with ID:{id} was not found!");
            //Remove product from sale
            Sales.Remove(product);

            return Sales;
        }

        //THIS METHOD IS FOR RETURNING ANY PRODUCT FROM SALE
        public List<Sale> SaleWithDraw(int saleId)
        {
            Console.Write("Enter number of Product to return: ");
            int num = int.Parse(Console.ReadLine()!);
            for (int i = 0; i < num; i++)
            {
                //Filter sales by id and Check if there is no sale in this range
                var sale = Sales.FirstOrDefault(x => x.Id == saleId) ?? throw new Exception($"Sale with ID:{saleId} was not found!");
                
                Console.Write("Enter saleItem's ID:");
                var saleItemId = int.Parse(Console.ReadLine()!);

                Console.Write("Enter saleItem's count:");
                var count = int.Parse(Console.ReadLine()!);
                //Filter saleItems by id and Check if there is no sale in this range
                var saleItem = SaleItems.FirstOrDefault(x => x.Id == saleItemId) ?? throw new Exception($"Product with ID:{saleItemId} was not found!");
                
                if(count > saleItem.Count)
                {
                     throw new Exception("Count can not be less than saleItem's count!");
                }
                //Reduce saleItem's count in sale after returning product
                saleItem.Count -= count;
                if(saleItem.Count == 0)
                {
                    sale.Items.Remove(saleItem);
                }
                if(sale.Items.Count == 0)
                {
                    Sales.Remove(sale);
                }
                //Reduce total amount of sale after returnin product
                sale.Amount -= count * saleItem.SaleProduct!.Price; 
                //Increases count of products in stock
                saleItem.SaleProduct!.Count += count;
            }
            return Sales;
        }

        //RETURN SALES WITHIN THE ID
        public List<Sale> GetSaleById(int id)
        {
            //Filter sales by id and Check if there is no sale in this range
            var sale = Sales.Where(x => x.Id == id).ToList() ?? throw new Exception($"Sale with ID:{id} was not found!");
            return sale;
        }

        //RETURN PRODUCTS IN SALE
        public List<SaleItem> GetSaleItemsBySaleId(int id)
        {
            //Filter sales by id and Check if there is no sale
            var saleItems = Sales.FirstOrDefault(x => x.Id == id) ?? throw new Exception($"SaleItem with ID:{id} is not found!");
            
            return saleItems.Items;
        }
        public List<Sale> GetSalesByPriceRange(decimal min, decimal max)
        {
            //Filter sales within the price range
            var price = Sales.Where(x => x.Amount >= min && x.Amount <= max).ToList();

            //Check if there is no sale in this range
            if (price == null)  throw new Exception($"No matching sales found!");

            return price;
        }
        public List<Sale> GetSalesByDateRange(DateTime min, DateTime max)
        {
            //Chech if min date is greater than max date
            if(min > max)
            {
                throw new Exception("Min date can not be greater than max date!");
            }
            //Filter sales within the date range
            var date = Sales.Where(x => x.Date >= min && x.Date <= max).ToList();

            //Check if there is no sale in this range
            if (date == null) throw new Exception($"No matching sales found!");

            return date;
        }
        public List<Sale> GetSalesByDate(DateTime date)
        {
            //Filter sales by date
            var dateTime = Sales.Where(x => x.Date == date).ToList();

            //Check if there is no sale in this date
            if (dateTime == null) throw new Exception($"Sale with date:{dateTime} is not found!");

            return dateTime;
        }
    }
}
