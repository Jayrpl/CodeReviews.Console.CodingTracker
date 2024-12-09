﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Dapper;

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
                                           Id INTEGER KEY AUTOINCREMENT,
                                           StartTime DATE,
                                           EndTime DATE,
                                           CalculateDuration Duration LONG)";

                tableCMD.ExecuteNonQuery();
            }
        }

        public void Delete()
        {
            Console.Clear();
            ShowAllRecords();

            // take user input (number)
            var recordId = 0;
            using (var connection = new SqliteConnection(connectionString))
            {
                string sql = $"DELETE * FROM coding_time WHERE Id =  '{recordId}'";

                int rowsAffected = connection.Execute(sql, new { RecordId = recordId });

                Console.WriteLine($"Rows affected: {rowsAffected}");
            }
        }

        public void Update() 
        {
            ShowAllRecords();

            //Get user input
            var recordId = 0;

            using (var connection = new SqliteConnection( connectionString))
            {
                connection.Open();

                string sql = "UPDATE coding_time SET StartTime = @StartTime, EndTime = @EndTime, Duration = @Duration WHERE Id = @recordId";
                
                //Get user inputted date
                string sDate = "";
                string eDate = "";
                CodingSession session = new CodingSession();
                long duration = session.CalculateDuration(sDate, eDate).Ticks;

                var parameters = new
                {
                    RecordId = recordId,
                    StartDate = sDate,
                    EndDate = eDate,
                    Duration = duration
                };

                int rowsAffected = connection.Execute(sql, parameters);

                Console.WriteLine($"{rowsAffected} row(s) updated.");
            }
        }

        public void ShowAllRecords()
        {
            Console.Clear();

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = "SELECT * from coding_time";

                var tableData = connection.Query(tableCmd.CommandText);

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
            Console.Clear();

            // Get date input (start and end)
            string sDate = "";
            string eDate = "";

            // Calculate duration
            CodingSession session = new CodingSession();
            var duration = session.CalculateDuration(sDate, eDate).Ticks;

            // Use duration to insert
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string sql = $"INSERT INTO coding_time(startDate, endDate, duration VALUES('{sDate}', '{eDate}', '{duration})' ";

                var parameters = new
                {
                    StartDate = sDate,
                    EndDate = eDate,
                    Duration = duration
                };

                int rows_inserted = connection.Execute(sql, parameters);

                Console.WriteLine(rows_inserted);
            }
        }
    }
}
