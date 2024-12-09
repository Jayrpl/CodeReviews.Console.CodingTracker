using System;

namespace coding_tracker
{
    // Class responsible for receiving user input and validating it
    class UserInput
    {
        internal static string? GetStringInput();

        static void GetUserSelection()
        {
            Console.Clear();

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
                        GetAllRecords();
                        break;
                    case 2:
                        Insert();
                        break;
                    case 3:
                        Delete();
                        break;
                    case 4:
                        Update();
                        break;
                    default:
                        Console.WriteLine("\nInvalid command. Please type a number from 0 to 4.\n");
                        break;
                }
            }
        }
    }
}