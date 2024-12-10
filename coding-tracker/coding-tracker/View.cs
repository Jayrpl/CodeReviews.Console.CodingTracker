using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using Spectre.Console.Cli;

namespace coding_tracker
{
    internal class View
    {
        internal static void GetUserSelection()
        {
            Console.Clear();
            Controller controller = new Controller();

            bool exit = false;

            while (!exit)
            {
                var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("[yellow]What would you like to do?[/]")
                .PageSize(10)
                .MoreChoicesText("[grey](Please choose an option)[/]")
                .AddChoices(new[]
                {
                    "View All Records", "Insert", "Delete", "Update", "Exit",
                }));
                switch (choice)
                {
                    case "Exit":
                        Console.WriteLine("See ya!");
                        exit = true;
                        Environment.Exit(0);
                        break;
                    case "View All Records":
                        DisplayRecords(controller.ShowAllRecords());
                        break;
                    case "Insert":
                        controller.Insert();
                        break;
                    case "Delete":
                        controller.Delete();
                        break;
                    case "Update":
                        controller.Update();
                        break;
                    default:
                        Console.WriteLine("Invalid command");
                        break;
                }
            }
        }


            //while (!exit)
            //{
            //    Console.WriteLine("\n\nMAIN MENU");
            //    Console.WriteLine("\nWhat would you like to do?");
            //    Console.WriteLine("\nType 0 to Close Application");
            //    Console.WriteLine("\nType 1 to View All Records");
            //    Console.WriteLine("\nType 2 to View Insert Record");
            //    Console.WriteLine("\nType 3 to View Delete Record");
            //    Console.WriteLine("\nType 4 to View Update Record");

            //    int chosenOption;

            //    while (!int.TryParse(Console.ReadLine(), out chosenOption))
            //    {
            //        Console.WriteLine("Please type a number between 0 and 3");
            //    }

            //    switch (chosenOption)
            //    {
            //        case 0:
            //            Console.WriteLine("See ya!");
            //            exit = true;
            //            Environment.Exit(0);
            //            break;
            //        case 1:
            //            controller.ShowAllRecords();
            //            break;
            //        case 2:
            //            controller.Insert();
            //            break;
            //        case 3:
            //            controller.Delete();
            //            break;
            //        case 4:
            //            controller.Update();
            //            break;
            //        default:
            //            Console.WriteLine("\nInvalid command. Please type a number from 0 to 4.\n");
            //            break;
            //    }

        internal static void DisplayRecords(List<CodingSession> tableData)
        {
            var table = new Table();
            table.AddColumn("All entries:");

            foreach (var data in tableData)
            {
                var id = data.Id;
                string sTime = data.StartTime;
                string eTime = data.EndTime;
                string date = data.Date;

                var span = TimeSpan.FromTicks(data.Duration);
                var mins = span.Minutes;
                var hours = span.Hours;
                Console.WriteLine("--------------------------------------------------------------------------------------------------------");
                Console.WriteLine($"Id:{id} - Date: {date} - Start Time: {sTime} - End Time: {eTime} - Duration: {hours} hours {mins} mins");
                Console.WriteLine("--------------------------------------------------------------------------------------------------------\n");

            }
        }

    }
}

