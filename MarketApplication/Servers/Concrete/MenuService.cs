using ConsoleTables;
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
            var table = new ConsoleTable("ID", "Name", "Count", "Price");

            foreach (var doctor in MarketService.GetProduct())
            {
                table.AddRow(doctor.Id, doctor.Name, doctor.Count, doctor.Price);
            }  

            table.Write();
        }
        public static void MenuAddProduct()
        {
            try
            {
                Console.Write("Enter product's name: ");
                string name = Console.ReadLine()!;

                Console.Write("Enter product's count: ");
                int count = int.Parse(Console.ReadLine()!);

                Console.Write("Enter product's price: ");
                decimal price = decimal.Parse(Console.ReadLine()!);

                MarketService.AddProduct(name, price, count);
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
    }
}
