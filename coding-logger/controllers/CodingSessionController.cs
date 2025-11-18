using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spectre.Console;

namespace coding_logger
{

    public class CodingSessionController
    {
        // The connection string tells SQLite where to find or create the database file.
        // "Data Source" specifies the path. If 'coding-logger.db' doesn't exist, it will be created automatically.
        string connectionString = @"Data Source=coding-logger.db";

        public void GetAllRecords()
        {
			// Create a Spectre Console Table
			var table = new Table();
			table.Border(TableBorder.Rounded);
			
			// Add the desired columns to the table, with styling
			table.AddColumn("[yellow]ID[/]");
			table.AddColumn("[yellow]Start Time[/]");
			table.AddColumn("[yellow]End Time[/]");
			table.AddColumn("[yellow]Duration[/]");

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

				// Get all current records
                tableCmd.CommandText = $"SELECT * FROM coding_logger";

                // ExecuteReader() runs the SELECT statement and returns a data reader for reading the results row by row.
                SqliteDataReader reader = tableCmd.ExecuteReader();

                if (reader.HasRows)
                {
					// While there are records in the DB table, add each row to the Spectre Console Table
                    while (reader.Read())
                    {
					TimeSpan duration = DateTime.Parse(reader.GetString(2)) - DateTime.Parse(reader.GetString(1));
				
					// Spectre Table Rows/Data are required to be of type STRING
					table.AddRow(
						reader.GetString(0),
						$"[cyan]{DateTime.Parse(reader.GetString(1))}[/]",		
						$"[cyan]{DateTime.Parse(reader.GetString(2))}[/]",		
						$"[cyan]{duration}[/]"		
					);

					// Display the table in the console
					AnsiConsole.Write(table);
					AnsiConsole.MarkupLine("Press any key to continue.");
                    }
                }
                else
                {
                    AnsiConsole.MarkupLine("No rows found");
                }

                connection.Close();
            }
        }

        public void Insert()
        {
			var startTime = AnsiConsole.Ask<string>(
				"Enter the [green]start time[/] of your coding session. Must be in format 'yyyy-mm-dd hh:mm:ss' (24hr)"
			);

			var endTime = AnsiConsole.Ask<string>(
				"Enter the [green]end time[/] of your coding session. Must be in format 'yyyy-mm-dd hh:mm:ss' (24hr)"
			);

			TimeSpan durationToInsert = DateTime.Parse(endTime) - DateTime.Parse(startTime);

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                //INSERT INTO DB USING PARAMETERIZED QUERIES
                tableCmd.CommandText =
                    $"INSERT INTO coding_logger (startTime, endTime, duration) VALUES (@startTime, @endTime, @duration)";
                tableCmd.Parameters.AddWithValue("@startTime", startTime);
                tableCmd.Parameters.AddWithValue("@endTime", endTime);
                tableCmd.Parameters.AddWithValue("@duration", durationToInsert.TotalSeconds);

                tableCmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void Delete()
        {
            Console.Clear();
            GetAllRecords();

            Console.WriteLine(
                "\n\nPlease type the id of the record you want to delete or type 0 to return to the Main Menu\n\n"
            );

            var recordId = Console.ReadLine();

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = $"DELETE FROM coding_logger WHERE id = '{recordId}'";

                int rowCount = tableCmd.ExecuteNonQuery();

                if (rowCount == 0)
                {
                    Console.WriteLine($"\n\nRecord with Id {recordId} doesn't exist.\n\n");
                    Delete();
                }
            }

            Console.WriteLine($"\n\nRecord with Id {recordId} deleted.\n\n");
        }

        public void Update()
        {
            Console.Clear();
            GetAllRecords();

            Console.WriteLine(
                "\n\nPlease type the id of the record you want to update or type 0 to return to the Main Menu\n\n"
            );

            var recordId = Console.ReadLine();

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var checkCmd = connection.CreateCommand();
                checkCmd.CommandText =
                    $"SELECT EXISTS(SELECT 1 FROM coding_logger WHERE id = {recordId})";
                // return the value from the DB. 0 == false, 1 == true
                int checkQuery = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (checkQuery == 0)
                {
                    Console.WriteLine($"\n\nRecord with Id {recordId} doesn't exist.\n\n");
                    connection.Close();
                    Update();
                }

                // Need to get the new date values the user wants to input.
                Console.WriteLine("Please enter a new start time. Must be in format 'yyyy-mm-dd hh:mm:ss' (24hr)");
                var newStartTime = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("Please enter a new end time. Must be in format 'yyyy-mm-dd hh:mm:ss' (24hr)");
                var newEndTime = DateTime.Parse(Console.ReadLine());

                TimeSpan newDuration = newEndTime - newStartTime;

                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    $"UPDATE coding_logger SET startTime = '{newStartTime.ToString()}', endTime = '{newEndTime.ToString()}', duration = {Convert.ToInt32(newDuration.TotalSeconds)} WHERE id = {recordId}";

                tableCmd.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}