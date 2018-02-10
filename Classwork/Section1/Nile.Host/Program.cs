/*
 * Logan Oge
 * ITSE 1430
 */
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
                    case 'L': ListMovie(); break;

                    //case 'a':
                    case 'A': AddMovie(); break;

                    //case 'r':
                    case 'R': RemoveMovie(); break;

                    //case 'q':
                    case 'Q': quit = true; break;
                };
            };
        }

        private static char DisplayMenu()
        {
            do
            {
                Console.WriteLine("L)ist Movie");
                Console.WriteLine("A)dd Movie");
                Console.WriteLine("R)emove Movie");
                Console.WriteLine("Q)uit");

                string input = Console.ReadLine();

                input = input.Trim();
                input = input.ToUpper();

                if (String.Compare(input , "L", true) == 0)
                    return input[0];
                else if (input == "A")
                    return input[0];
                else if (input == "R")
                    return input[0];
                else if (input == "Q")
                    return input[0];
                
                Console.WriteLine("Please choose a valid option");
            } while (true);
        }

        static void AddMovie()
        {
            //Get name
            _name = ReadString("Enter name: ", true);

            //get length
            _length = ReadDouble("Enter Length: ", 0);

            //Get description
            _description = ReadString("Enter optional description: ", false);

            // get isOwned
            _isOwned = ReadBool();

        }

        static void RemoveMovie()
        {
            Console.WriteLine("Are you sure you want to delete the movie?");
            Console.WriteLine("Enter Y or N.");

            //getting reply
            var answer = Console.ReadLine();
            answer = answer.Trim();
            answer = answer.ToUpper();

            //validate 
            if (String.Compare(answer, "Y", true) == 0)
                _name = "";
            else if (answer == "N")
                return;
            else
                Console.WriteLine("Please choose a valid option");


        }

        static bool ReadBool()
        {
            Console.WriteLine("Do you own this movie?");
            Console.WriteLine("Enter Y/N.");
            do
            {
                string input = Console.ReadLine();

                if (!String.IsNullOrEmpty(input))
                {
                    switch (Char.ToUpper(input[0]))
                    {
                        case 'Y':
                        return true;
                        case 'N':
                        return false;
                    };

                };

                Console.WriteLine("enter either Y or N");
            } while (true);
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


        private static double ReadDouble( string message, double minValue )
        {
            do
            {
                Console.Write(message);
                string value = Console.ReadLine();

                double result;
                if (Double.TryParse(value, out result))
                {
                    //if not required or not empty
                    if (result >= minValue)
                        return result;
                };

                string msg = String.Format("Value must be >= {0}", minValue);
                Console.WriteLine(msg);
            } while (true);
        }



        private static void ListMovie()
        {

            if (!String.IsNullOrEmpty(_name))
            {
                string msg = $"{_name} [{_length}]";
                Console.WriteLine(msg);
                if (!String.IsNullOrEmpty(_description))
                    Console.WriteLine(_description);
                if (_isOwned == true)
                    Console.WriteLine("Owned");
                else
                    Console.WriteLine("not Owned");
            } else
                Console.WriteLine("No Movie");
        }

        //data for a product
        static string _name;
        static double _length;
        static string _description;
        static bool _isOwned;
    }
}
