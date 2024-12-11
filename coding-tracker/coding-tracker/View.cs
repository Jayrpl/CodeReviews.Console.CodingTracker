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

