using System;
using System.Globalization;

namespace coding_tracker
{
    // Class responsible for receiving user input and validating it
    static class UserInput
    {
        internal static string GetDateInput()
        {
            Console.WriteLine("Please insert the date in dd/mm/yyyy form.");

            string? input = Console.ReadLine();

            if (input == "0") GetUserSelection();

            while (!DateTime.TryParseExact(input, "dd/mm/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out _))
            {
                Console.WriteLine("\n\nInvalid Date. Please insert the date in dd/mm/yyyy form.\n\n");
                input = Console.ReadLine();
            }

            return input;
        }

        internal static int GetNumberInput(string message)
        {
            Console.WriteLine(message);

            int input;

            while (!int.TryParse(Console.ReadLine(), out input))
            {
                Console.WriteLine(message);
            }

            return input;
        }

        internal static void GetUserSelection()
        {
            Console.Clear();
            Controller controller = new Controller(); 

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n\nMAIN MENU");
                Console.WriteLine("\nWhat would you like to do?");
                Console.WriteLine("\nType 0 to Close Application");
                Console.WriteLine("\nType 1 to View All Records");
                Console.WriteLine("\nType 2 to View Insert Record");
                Console.WriteLine("\nType 3 to View Delete Record");
                Console.WriteLine("\nType 4 to View Update Record");

                int chosenOption;

                while (!int.TryParse(Console.ReadLine(), out chosenOption))
                {
                    Console.WriteLine("Please type a number between 0 and 3");
                }

                switch (chosenOption)
                {
                    case 0:
                        Console.WriteLine("See ya!");
                        exit = true;
                        Environment.Exit(0);
                        break;
                    case 1:
                        controller.ShowAllRecords();
                        break;
                    case 2:
                        controller.Insert();
                        break;
                    case 3:
                        controller.Delete();
                        break;
                    case 4:
                        controller.Update();
                        break;
                    default:
                        Console.WriteLine("\nInvalid command. Please type a number from 0 to 4.\n");
                        break;
                }
            }
        }
    }
}