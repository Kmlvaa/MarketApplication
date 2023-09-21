using ConsoleTables;
using MarketApplication.Data.Enum;
using MarketApplication.Data.Models;
using MarketApplication.Servers.Abstract;
using System;
using System.Diagnostics;
using System.Linq;

namespace MarketApplication.Servers.Concrete
{
    public class MenuService
    {
        private static IMarket MarketService = new MarketService();
           
        public static void MenuGetProduct()
        {
            var table = new ConsoleTable("ID", "Name", "Category", "Count", "Price");

            foreach (var product in MarketService.GetProducts())
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

                MarketService.UpdateProduct(id, count, price, name,category);
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

            var table = new ConsoleTable("ID", "Count", "Amount", "Date");

            foreach (var sale in MarketService.GetSales())
            {
                table.AddRow(sale.Id,sale.Items.Count, sale.Amount, sale.Date);
            }
            table.Write();
        }
        public static void MenuDeleteSales()
        {
            try
            {
                Console.Write("Enter sale's ID: ");
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
            try
            {
                Console.Write("Enter sale's ID:");
                int saleId = int.Parse(Console.ReadLine()!);
                
                MarketService.SaleWithDraw(saleId);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void MenuGetSaleById()
        {
            try
            {
                Console.Write("Enter sale's ID: ");
                int id = int.Parse(Console.ReadLine()!);

                var table = new ConsoleTable("ID", "Count", "Amount", "Date");

                foreach (var sale in MarketService.GetSaleById(id))
                {
                    table.AddRow(sale.Id, sale.Items.Count, sale.Amount, sale.Date);
                }
                table.Write();
                Console.WriteLine("---------------------------------------");
                var table2 = new ConsoleTable("SaleItem ID", "Product", "Count", "Price");
                foreach (var saleItem in MarketService.GetSaleItemsBySaleId(id))
                {
                    table2.AddRow(saleItem.Id, saleItem.SaleProduct!.Name, saleItem.Count, saleItem.SaleProduct.Price);
                }
                table2.Write();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void MenuGetSalesByPriceRange()
        {
            try
            {
                Console.Write("Enter min amount: ");
                decimal min = decimal.Parse(Console.ReadLine()!);

                Console.Write("Enter max amount: ");
                decimal max = decimal.Parse(Console.ReadLine()!);

                var table = new ConsoleTable("ID", "Count", "Amount", "Date");

                foreach (var sale in MarketService.GetSalesByPriceRange(min, max))
                {
                    table.AddRow(sale.Id, sale.Items.Count, sale.Amount, sale.Date);
                }
                table.Write();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void MenuGetSalesByDateRange()
        {
            try
            {
                Console.Write("Enter min DateTime: ");
                DateTime min = DateTime.ParseExact(Console.ReadLine()!, "dd.MM.yyyy HH:mm:ss", null);

                Console.Write("Enter max DateTime: ");
                DateTime max = DateTime.ParseExact(Console.ReadLine()!, "dd.MM.yyyy HH:mm:ss", null);

                var table = new ConsoleTable("ID", "Count", "Amount", "Date");

                foreach (var sale in MarketService.GetSalesByDateRange(min, max))
                {
                    table.AddRow(sale.Id, sale.Items.Count, sale.Amount, sale.Date);
                }
                table.Write();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void MenuGetSalesByDate()
        {
            try
            {
                Console.Write("Enter sale's date: ");
                var date = DateTime.ParseExact(Console.ReadLine()!,"dd.MM.yyyy HH:mm:ss", null); 

                var table = new ConsoleTable("ID", "Count", "Amount", "Date");

                foreach (var sale in MarketService.GetSalesByDate(date))
                {
                    table.AddRow(sale.Id, sale.Items.Count, sale.Amount, sale.Date);
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
