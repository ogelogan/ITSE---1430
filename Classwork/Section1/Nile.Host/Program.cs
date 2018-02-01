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
                switch (Char.ToUpper(choice))
                {
                    //case 'l':
                    case 'L': ListProducts(); break;

                    //case 'a':
                    case 'A': AddProduct(); break;

                    //case 'q':
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

                input = input.Trim();
                input = input.ToUpper();
                //input.ToLower();

                //padding
                //input = input.PadLeft(10);

                //Starts with
                //input.StartsWith(@"\");
                //input.EndsWith(@"\");

                //if (input == "L")
                if (String.Compare(input , "L", true) == 0)
                    return input[0];
                else if (input == "A")
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

                string msg = String.Format("Value must be >= {0}", minValue);
                Console.WriteLine(msg);
                //Console.WriteLine("Value must be >= {0}", minValue);
            } while (true);
        }

        private static void ListProducts()
        {
            //Are there any products?
            //if (_name != null && _name != "")
            //if (_name != null && _name != String.Empty)
            //if (_name != null && _name.Length ==0)
            if (!String.IsNullOrEmpty(_name))
            {

                //display a product - name - [$price]
                //                    <description>
                //var msg = String.Format("{0} [${1}]", _name, _price);
                //Console.WriteLine(msg);

                //string concatenation
                //var msg = _name + " [$" + _price + "]";
                //Console.WriteLine(msg);

                //string concat part 2
                //var msg = String.Concat(_name, " [$", _price, "]");
                //Console.WriteLine(msg);

                //string interpolation
                string msg = $"{_name} [${_price}]";
                Console.WriteLine(msg);

                //Console.WriteLine(_name);
                //Console.WriteLine(_price);

                if (!String.IsNullOrEmpty(_description))
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
