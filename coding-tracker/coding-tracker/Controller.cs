using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace coding_tracker
{
    internal class Controller
    {
        string connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");

        public void InitialiseDatabase()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var tableCMD = connection.CreateCommand();

                tableCMD.CommandText = @"CREATE TABLE IF NOT EXISTS coding_time (
                                           Id INTEGER KEY AUTOINCREMENT,
                                           StartTime DATE,
                                           EndTime DATE,
                                           Duration LONG)";

                tableCMD.ExecuteNonQuery();
            }
        }

        public void Delete()
        {

        }

        public void Update() 
        {
        
        }

        public void ShowAllRecords()
        {
            Console.Clear();

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = "SELECT * from coding_time";

                List<CodingSession> tableData = new();
                SqliteDataReader reader = tableCmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tableData.Add(
                            new CodingSession()
                            {
                                id = reader.GetInt32(0),
                                duration = reader.GetInt32(1),
                                startTime = reader.GetDateTime(2),
                                endTime = reader.GetDateTime(3)
                            });

                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }

                connection.Close();

                Console.WriteLine("----------------------------------------------\n");

                foreach (var data in tableData)
                {
                    Console.WriteLine($"{data.id} - Start Time: {data.startTime} - End Time: {data.endTime} - Duration: {data.duration}");
                }

                Console.WriteLine("----------------------------------------------\n");

            }

        }

        

        public void Insert()
        {

        }
    }
}
