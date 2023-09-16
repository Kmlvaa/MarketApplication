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
           
        public static void MenuShowProduct()
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

                Console.Write("Enter product's category: ");
                Category category = (Category)Enum.Parse(typeof(Category), Console.ReadLine()!);

                Console.Write("Enter product's count: ");
                int count = int.Parse(Console.ReadLine()!);

                Console.Write("Enter product's price: ");
                decimal price = decimal.Parse(Console.ReadLine()!);

                MarketService.UpdateProduct(id, count, price, name, category);
                Console.WriteLine($"Product with ID:{id} is updated!");
            }
            catch( Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void MenuShowProductsByCategory()
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

                foreach (var product in MarketService.ShowProductsByCategory(category))
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
        public static void MenuShowProductsByName()
        {
            try
            {
                Console.Write("Enter product's name: ");
                string name = Console.ReadLine()!;

                var table = new ConsoleTable("ID", "Name", "Category", "Count", "Price");

                foreach (var product in MarketService.ShowProductsByName(name))
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
        public static void MenuShowProductsByPriceRange()
        {
            try
            {
                Console.Write("Enter minimum price: ");
                decimal min = decimal.Parse(Console.ReadLine()!);

                Console.Write("Enter maximum price: ");
                decimal max = decimal.Parse(Console.ReadLine()!);

                var table = new ConsoleTable("ID", "Name", "Category", "Count", "Price");

                foreach (var product in MarketService.ShowProductsByPriceRange(min, max))
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


    }
}
