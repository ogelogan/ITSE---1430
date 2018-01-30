using System;

namespace Nile.Host
{
    class Program
    {
        static void Main( string[] args )
        {
            bool quit = false;
            while (!quit)
            {
                //Display Menu

                char choice = DisplayMenu();

                //Process menu selection
                switch (choice)
                {
                    case 'l':
                    case 'L': ListProducts(); break;

                    case 'a':
                    case 'A': AddProduct(); break;

                    case 'q':
                    case 'Q': quit = true; break;
                };
            };
        }

        private static char DisplayMenu()
        {
            do
            {
                Console.WriteLine("L)ist Products");
                Console.WriteLine("A)dd Product");
                Console.WriteLine("Q)uit");

                string input = Console.ReadLine();

                if (input == "L" || input == "l")
                    return input[0];
                else if (input == "A" || input == "a")
                    return input[0];
                else if (input == "Q")
                    return input[0];
                
                Console.WriteLine("Please choose a valid option");
            } while (true);
        }

        static void AddProduct()
        {
            //Get name
            _name = ReadString("Enter name: ", true);

            //get Price
            _price = ReadDecimal("Enter Price: ", 0);

            //Get description
            _description = ReadString("Enter optional description: ", false);

        }

        private static string ReadString(string message, bool isRequired)
        {
            do
            {
                Console.Write(message);
                string value = Console.ReadLine();

                //if not required or not empty
                if (!isRequired || value != "")
                    return value;

                Console.WriteLine("Value is required");
            } while (true);
        }

        private static decimal ReadDecimal( string message, decimal minValue )
        {
            do
            {
                Console.Write(message);
                string value = Console.ReadLine();

                decimal result;
                if (Decimal.TryParse(value, out result))
                {
                    //if not required or not empty
                    if (result >= minValue)
                        return result;
                };

                Console.WriteLine("Value must be >= {0}", minValue);
            } while (true);
        }

        private static void ListProducts()
        {
            //Are there any products?
            if (_name != null && _name != "")
            {

                //display a product
                Console.WriteLine(_name);
                Console.WriteLine(_price);
                Console.WriteLine(_description);
            } else
                Console.WriteLine("No products");
        }

        //data for a product
        static string _name;
        static decimal _price;
        static string _description;
    }
}
