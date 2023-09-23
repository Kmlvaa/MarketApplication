using MarketApplication.Data.Enum;
using MarketApplication.Data.Models;
using MarketApplication.Servers.Abstract;
using System.ComponentModel;

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
            //Creates new Sale object
            var sale = new Sale();
            Sales.Add(sale);
            int saleID = sale.Id;
            try
            {
                //checks if user input 0 as an count 
                if (num == 0)
                {
                    throw new Exception("End!");
                }
                decimal amount = 0;
                int count = 0;
               
                for (int i = 0; i < num; i++)
                {
                    Console.Write("Enter product's id: ");
                    int id = int.Parse(Console.ReadLine()!);

                    //Find product by ID
                    var product = Products.FirstOrDefault(x => x.Id == id); 

                    if (product == null)
                    {
                        throw new Exception($"Product with ID:{id} was not found!");
                    }

                    Console.Write("Enter product's count: ");
                    count = int.Parse(Console.ReadLine()!);

                    if (count == 0)
                    {
                        throw new Exception("Could not add 0 product!");
                    }

                    if (count > product.Count)
                    {
                        throw new Exception("The number of products cannot exceed the number in stock!");
                    }
                    // Clone the product to avoid changing product in sale
                    var cloneProduct = (Product)product.Clone();

                    //Create sale item
                    var saleItem = new SaleItem
                    {
                        Count = count,
                        SaleProduct = cloneProduct
                    };

                    //Adds saleItems to the sale
                    sale.Items.Add(saleItem);

                    //Update product count after sale
                    product.Count -= count;

                    //Calculate total amount of sale
                    decimal price = count * cloneProduct.Price;
                    amount += price;

                    SaleItems.Add(saleItem);
                }

                sale.Amount = amount;

            }
            catch (Exception ex)
            {
                DeleteSale(saleID);
                Console.WriteLine(ex.Message);
            }

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
            var sale = Sales.FirstOrDefault(x => x.Id == id) ?? throw new Exception($"Sale with ID:{id} was not found!");
            //Remove product from sale
            foreach (var item in sale.Items)
            {
                var product = Products.FirstOrDefault(x => x.Id == item.SaleProduct!.Id);

                if(product != null)
                {
                    product.Count += item.Count;//birde eleyesen .
                }
            }
            Sales.Remove(sale);

            return Sales;
        }

        //THIS METHOD IS FOR RETURNING ANY PRODUCT FROM SALE
        public List<Sale> SaleWithDraw(int saleId)
        {
            Console.Write("Enter number of Product to return: ");
            int num = int.Parse(Console.ReadLine()!);
            if (num > SaleItems.Count) throw new Exception($"Can not return more than {SaleItems.Count} product!");
            // Find the sale by ID
            var sale = Sales.FirstOrDefault(x => x.Id == saleId);

            if (sale == null)
            {
                throw new Exception($"Sale with ID:{saleId} was not found!");
            }

            for (int i = 0; i < num; i++)
            {
                Console.Write("Enter saleItem's ID:");
                var saleItemId = int.Parse(Console.ReadLine()!);


                Console.Write("Enter saleItem's count:");
                var count = int.Parse(Console.ReadLine()!);

                // Find the sale item by ID within the sale
                var saleItem = sale.Items.FirstOrDefault(x => x.Id == saleItemId);

                if (saleItem == null)
                {
                    throw new Exception($"Sale Item with ID:{saleItemId} was not found in the sale!");
                }

                if (count > saleItem.Count)
                {
                    throw new Exception("Count cannot be greater than the saleItem's count!");
                }

                // Reduce saleItem's count in the sale after returning the product
                saleItem.Count -= count;

                // Increase the count of products in stock
                var updateProduct = Products.FirstOrDefault(x => x.Id == saleItem.Id);
                if (updateProduct != null)
                {
                    updateProduct.Count += count;
                }
                // Reduce the total amount of the sale after returning the product
                sale.Amount -= count * saleItem.SaleProduct!.Price;

                if (saleItem.Count == 0)
                {
                    // Remove the saleItem from the sale if its count becomes zero
                    sale.Items.Remove(saleItem);
                }

                if (sale.Items.Count == 0)
                {
                    // Remove the sale from Sales if it has no items left
                    Sales.Remove(sale);
                    break; // Exit the loop if the sale has been removed
                }


                //saleItem.SaleProduct!.Count += count;
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
