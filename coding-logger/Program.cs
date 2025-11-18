//using Spectre.Console;

namespace coding_logger
{
    class Program
    {
        static void Main(string[] args)
        {
/*
            using (var connection = new SqliteConnection(connectionString))
            {
                // Open the connection to the SQLite database file
                connection.Open();
                
                // Create a SqliteCommand object that lets you run SQL commands on this connection.
                var tableCmd = connection.CreateCommand();
            
                tableCmd.CommandText =
                    @"CREATE TABLE IF NOT EXISTS coding_logger (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    startTime TEXT,
                    endTime TEXT,
                    duration INTEGER
                )";
                
                // ExecuteNonQuery() runs the SQL command but doesn’t return any rows (used for CREATE, INSERT, DELETE, etc.).
                tableCmd.ExecuteNonQuery();
                connection.Close();
            }
*/

			CodingSessionController _csController = new CodingSessionController();
            _csController.GetAllRecords();
        }
    }
}

/*
Configuration File -> model -> database/table creation -> CRUD controller (where the operations will happen) -> 
TableVisualisationEngine (where the consoleTableExt code will be run) and finally: validation of data.
*/

// Enter a date and time (e.g., 2025-11-10 18:47:00)

/**
 * Will need to get the starttime, endtime manually from the user
 *  This includes both the date AND the time
 *  Format should be -> 2025-11-10 18:47:00
 */

//     dotnet add package Microsoft.Data.Sqlite

