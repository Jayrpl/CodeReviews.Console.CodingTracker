using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Dapper;
using System.Reflection.Metadata;

namespace coding_tracker
{
    internal class Controller
    {
        string connectionString = ConfigurationManager.AppSettings.Get("ConnectionString") + ConfigurationManager.AppSettings.Get("DatabasePath");

        public void InitialiseDatabase()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var tableCMD = connection.CreateCommand();

                tableCMD.CommandText = @"CREATE TABLE IF NOT EXISTS coding_time (
                                           Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                           Date TEXT,
                                           StartTime TEXT,
                                           EndTime TEXT,
                                           Duration INTEGER)";

                tableCMD.ExecuteNonQuery();
            }
        }

        public void Delete()
        {
            Console.Clear();
            ShowAllRecords();

            // take user input (number)
            var recordId = UserInput.GetNumberInput("Please input the record id number that you would like to delete.");
            using (var connection = new SqliteConnection(connectionString))
            {
                string sql = "DELETE FROM coding_time WHERE Id = @RecordId";

                int rowsAffected = connection.Execute(sql, new { RecordId = recordId });

                Console.WriteLine($"Rows affected: {rowsAffected}");
            }
        }

        public void Update() 
        {
            ShowAllRecords();

            //Get user input
            var recordId = UserInput.GetNumberInput("Please input the record id number that you would like to update.");

            using (var connection = new SqliteConnection( connectionString))
            {
                connection.Open();

                // Get date input
                string date = UserInput.GetDateInput();
                string startTime = UserInput.GetTimeInput("Please enter the time you started in 24-hour format.");
                string endTime = UserInput.GetTimeInput("Please enter the time you started in 24-hour format.");
                string sTime = date + " " + startTime;
                string eTime = date + " " + endTime;

                // Calculate duration
                CodingSession session = new CodingSession();
                var duration = session.CalculateDuration(sTime, eTime).Ticks;

                string sql = "UPDATE coding_time SET Date = @Date, StartTime = @StartTime, EndTime = @EndTime, Duration = @Duration WHERE Id = @RecordId";

                var parameters = new
                {
                    RecordId = recordId,
                    Date = date,
                    StartTime = startTime,
                    EndTime = endTime,
                    Duration = duration
                };

                int rowsAffected = connection.Execute(sql, parameters);

                Console.WriteLine($"{rowsAffected} row(s) updated.");
            }
        }

        public List<CodingSession> ShowAllRecords()
        {
            Console.Clear();

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string command = "SELECT * from coding_time";

                List<CodingSession> tableData = connection.Query<CodingSession>(command).ToList();

                connection.Close();
                return tableData;
            }


        }

        public void Insert()
        {
            Console.Clear();

            // Get date input
            string date = UserInput.GetDateInput();
            string startTime = UserInput.GetTimeInput("Please enter the time you started in 24-hour format.");
            string endTime = UserInput.GetTimeInput("Please enter the time you finished in 24-hour format.");
            string sTime = date + " " + startTime;
            string eTime = date + " " + endTime;         

            // Calculate duration
            CodingSession session = new CodingSession();
            long duration = session.CalculateDuration(sTime, eTime).Ticks;

            // Use duration to insert
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string sql = "INSERT INTO coding_time(Date, StartTime, EndTime, Duration) VALUES(@Date, @StartTime, @EndTime, @Duration) ";

                var parameters = new
                {
                    Date = date,
                    StartTime = startTime,
                    EndTime = endTime,
                    Duration = duration
                };

                int rows_inserted = connection.Execute(sql, parameters);

                Console.WriteLine(rows_inserted);

                connection.Close();
            }
        }
    }
}
