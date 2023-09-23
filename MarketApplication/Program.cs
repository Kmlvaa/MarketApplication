using MarketApplication.Helper;

namespace MarketApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int selectedOption;

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Welcome to Market!");

            do
            {
                Console.WriteLine("----------------------------");
                Console.WriteLine("1. Manage product");
                Console.WriteLine("2. Manage sale");

                Console.WriteLine("0. Exit");
                Console.WriteLine("----------------------------");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Please, select an option:");
                while (!int.TryParse(Console.ReadLine(), out selectedOption))
                {
                    Console.WriteLine("Please enter valid option:");
                }

                switch (selectedOption)
                {
                    case 1:
                        SubMenuHelper.DisplayProductMenu();
                        break;
                    case 2:
                        SubMenuHelper.DisplaySaleMenu();
                        break;
                    case 0:
                        break;
                    default:
                        Console.ForegroundColor= ConsoleColor.Red;
                        Console.WriteLine("No such option!");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        break;
                }
            } while (selectedOption != 0);
        }
    }
}