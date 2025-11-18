using Microsoft.Data.Sqlite;

namespace coding_logger
{

    public class CodingSessionController
    {
        // The connection string tells SQLite where to find or create the database file.
        // "Data Source" specifies the path. If 'coding-logger.db' doesn't exist, it will be created automatically.
        string connectionString = @"Data Source=coding-logger.db";

        public void GetAllRecords()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText = $"SELECT * FROM coding_logger";

                List<CodingSession> tableData = new List<CodingSession>();

                // ExecuteReader() runs the SELECT statement and returns a data reader for reading the results row by row.
                SqliteDataReader reader = tableCmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tableData.Add(
                            new CodingSession(
                                Convert.ToInt32(reader.GetString(0)),
                                DateTime.Parse(reader.GetString(1)),
                                DateTime.Parse(reader.GetString(2))
                            )
                        );
                    }
                }
                else
                {
                    Console.WriteLine("No rows found");
                }

                connection.Close();

                Console.WriteLine("--------------------------\n");
                // Log each row in the tableData List to the console
                foreach (var row in tableData)
                {
                    Console.WriteLine(
                        $"{row.Id} - {row.Duration}"
                    );
                }

                Console.WriteLine("--------------------------\n");
            }
        }

        public void Insert()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                //INSERT INTO DB USING PARAMETERIZED QUERIES
                tableCmd.CommandText =
                    $"INSERT INTO coding_logger (startTime, endTime, duration) VALUES (@startTime, @endTime, @duration)";
                tableCmd.Parameters.AddWithValue("@startTime", "2025-11-15 16:57:00");
                tableCmd.Parameters.AddWithValue("@endTime", "2025-11-15 18:57:00");
                tableCmd.Parameters.AddWithValue("@duration", 7200);

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
                tableCmd.CommandText = $"DELETE FROM coding_sessions WHERE id = '{recordId}'";

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
                "\n\nPlease type the id of the record you want to delete or type 0 to return to the Main Menu\n\n"
            );

            var recordId = Console.ReadLine();

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var checkCmd = connection.CreateCommand();
                checkCmd.CommandText =
                    $"SELECT EXISTS(SELECT 1 FROM coding_sessions WHERE id = {recordId})";
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
                    $"UPDATE coding_sessions SET startTime = '{newStartTime}', endTime = '{newEndTime}', duration = {newDuration} WHERE id = {recordId}";

                tableCmd.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}