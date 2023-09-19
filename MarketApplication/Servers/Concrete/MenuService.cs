using ConsoleTables;
using MarketApplication.Data.Enum;
using MarketApplication.Data.Models;
using MarketApplication.Servers.Abstract;
using System;
using System.Linq;

namespace MarketApplication.Servers.Concrete
{
    public class MenuService
    {
        //private static IMarket MarketService = new MarketService();
           
        public static void MenuGetProduct()
        {
            var table = new ConsoleTable("ID", "Name", "Category", "Count", "Price");

            foreach (var product in MarketService.GetProduct())
            {
                table.AddRow(product.Id, product.Name, product.Categories, product.Count, product.Price);
            }  

            table.Write();
        }
        public static void MenuAddProduct()
        {
            try
            {
                Console.Write("Enter product's name: ");
                string name = Console.ReadLine()!;

                Console.Write("Enter product's category: ");
                Category category = (Category)Enum.Parse(typeof(Category), Console.ReadLine()!);

                Console.Write("Enter product's count: ");
                int count = int.Parse(Console.ReadLine()!);

                Console.Write("Enter product's price: ");
                decimal price = decimal.Parse(Console.ReadLine()!);

                MarketService.AddProduct(category, name, price, count);
                Console.WriteLine($"{count} {name} product was added. / Price is:{price}");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void MenuRemoveProduct()
        {
            try
            {
                Console.Write("Enter product's id: ");
                int id = int.Parse(Console.ReadLine()!);

                MarketService.DeleteProduct(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void MenuUpdateProduct()
        {
            try
            {
                Console.Write("Enter product's id: ");
                int id = int.Parse(Console.ReadLine()!);

                Console.Write("Enter product's name: ");
                string name = Console.ReadLine()!;

                //Console.Write("Enter product's category: ");
                //Category category = (Category)Enum.Parse(typeof(Category), Console.ReadLine()!);

                Console.Write("Enter product's count: ");
                int count = int.Parse(Console.ReadLine()!);

                Console.Write("Enter product's price: ");
                decimal price = decimal.Parse(Console.ReadLine()!);

                MarketService.UpdateProduct(id, count, price, name);
                Console.WriteLine($"Product with ID:{id} is updated!");
            }
            catch( Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void MenuGetProductsByCategory()
        {
            try
            {
                int count = 1;
                foreach (var item in Enum.GetNames(typeof(Category)))
                { 
                    Console.WriteLine($"{count++} - {item}");
                }
                Console.Write("Select a category: ");
                int category = int.Parse(Console.ReadLine()!);

                var table = new ConsoleTable("ID", "Name", "Category", "Count", "Price");

                foreach (var product in MarketService.GetProductsByCategory(category))
                {
                    table.AddRow(product.Id, product.Name, product.Categories, product.Count, product.Price);
                }

                table.Write();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public static void MenuGetProductsByName()
        {
            try
            {
                Console.Write("Enter product's name: ");
                string name = Console.ReadLine()!;

                var table = new ConsoleTable("ID", "Name", "Category", "Count", "Price");

                foreach (var product in MarketService.GetProductsByName(name))
                {
                    table.AddRow(product.Id, product.Name, product.Categories, product.Count, product.Price);
                }

                table.Write();
            }
            catch (Exception ex) 
            { 
               Console.WriteLine(ex.Message); 
            }
        }
        public static void MenuGetProductsByPriceRange()
        {
            try
            {
                Console.Write("Enter minimum price: ");
                decimal min = decimal.Parse(Console.ReadLine()!);

                Console.Write("Enter maximum price: ");
                decimal max = decimal.Parse(Console.ReadLine()!);

                var table = new ConsoleTable("ID", "Name", "Category", "Count", "Price");

                foreach (var product in MarketService.GetProductsByPriceRange(min, max))
                {
                    table.AddRow(product.Id, product.Name, product.Categories, product.Count, product.Price);
                }

                table.Write();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void MenuAddSales()
        {
            try
            {
                 Console.Write("Enter number of saleItems: ");
                 int num = int.Parse(Console.ReadLine()!);

                 MarketService.AddSale(num);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void MenuGetSales()
        {

            var table = new ConsoleTable("ID", "Amount", "Date");

            foreach (var sale in MarketService.GetSales())
            {
                table.AddRow(sale.Id, sale.Amount, sale.Date);
            }

            table.Write();
        }
        public static void MenuDeleteSales()
        {
            try
            {
                Console.Write("Enter product's ID: ");
                int id = int.Parse(Console.ReadLine()!);
                MarketService.DeleteSale(id);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void MenuSaleWithDraw()
        {
            Console.Write("Enter number of Product to return: ");
            int num = int.Parse(Console.ReadLine()!);
            MarketService.SaleWithDraw(num);
        }
    }
}
