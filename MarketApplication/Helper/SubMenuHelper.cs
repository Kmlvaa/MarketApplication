using MarketApplication.Servers.Concrete;
using System;

namespace MarketApplication.Helper
{
    internal class SubMenuHelper
    {
        public static void DisplayProductMenu() 
        {
            int selectedOption;

            do
            {
                Console.WriteLine("----------------------------");
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. Delete Product");
                Console.WriteLine("3. Update Product");
                Console.WriteLine("4. Show Product");
                Console.WriteLine("5. Show Product by category");
                Console.WriteLine("6. Show Product by name");
                Console.WriteLine("7. Show product by price range");

                Console.WriteLine("0. Exit");
                Console.WriteLine("----------------------------");
                Console.Write("Please, select an option:");

                while (!int.TryParse(Console.ReadLine(), out selectedOption))
                {
                    Console.WriteLine("Please enter valid option:");
                }

                switch (selectedOption)
                {
                    case 1:
                        MenuService.MenuAddProduct();
                        break;
                    case 2:
                        MenuService.MenuRemoveProduct();
                        break;
                    case 3:
                        MenuService.MenuUpdateProduct();
                        break;
                    case 4:
                        MenuService.MenuShowProduct();
                        break;
                    case 5:
                        MenuService.MenuShowProductsByCategory();
                        break;
                    case 6:
                        MenuService.MenuShowProductsByName();
                        break;
                    case 7:
                        MenuService.MenuShowProductsByPriceRange();
                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine("No such option!");
                        break;
                }
            } while (selectedOption != 0);
        }
        public static void DisplaySaleMenu()
        {
            int selectedOption;

            do{
                Console.WriteLine("----------------------------");
                Console.WriteLine("1. Add Sale");
                Console.WriteLine("2. Delete Sale");
                Console.WriteLine("3. Update Sale");
                Console.WriteLine("4. Show Sales");
                Console.WriteLine("5. Show Sale by id");
                Console.WriteLine("6. Show Sale by date");
                Console.WriteLine("7. Show Sales by date range");
                Console.WriteLine("8. Show Sales by price range");

                Console.WriteLine("0. Exit");
                Console.WriteLine("----------------------------");
                Console.Write("Please, select an option:");

                while (!int.TryParse(Console.ReadLine(), out selectedOption))
                {
                    Console.WriteLine("Please enter valid option:");
                }

                switch (selectedOption)
                {
                    case 1:
                        MenuService.MenuAddSales();
                        break;
                    case 2:
                        MenuService.MenuDeleteSales();
                        break;
                    case 3:
                        break;
                    case 4:
                        MenuService.MenuShowSales();
                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine("No such option!");
                        break;
                }
            } while (selectedOption != 0);
        }
    }
}
