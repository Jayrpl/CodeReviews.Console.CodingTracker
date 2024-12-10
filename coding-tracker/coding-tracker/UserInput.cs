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

            if (input == "0") return null;

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

        internal static string GetTimeInput(string message)
        {
            Console.WriteLine(message);

            string? input = Console.ReadLine();

            while (!IsValidTime(input))
            {
                Console.WriteLine("Is invalid time input. Please enter a 4-digit time in 24-hour format (eg 1200, 0630, 1830");
                input = Console.ReadLine();
            }

            return input;


        }

        private static bool IsValidTime(string? input)
        {
            // Validate the input
            if (string.IsNullOrEmpty(input) || input.Length != 4 || !int.TryParse(input, out _))
                return false;

            // Extract hour and minute
            int hour = int.Parse(input.Substring(0, 2));
            int minute = int.Parse(input.Substring(2, 2));

            // Validate hour (0-23) and minute (0-59)
            return hour >= 0 && hour < 24 && minute >= 0 && minute < 60;
        }
    }
}