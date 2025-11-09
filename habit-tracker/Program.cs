using System;
using System.Globalization;
using Microsoft.Data.Sqlite;

string connectionString = @"Data Source=habit-Tracker.db";

// The connection string tells SQLite where to find or create the database file.
// "Data Source" specifies the path. If "habit-tracker.db" doesn't exist, it will be created automatically.

using (var connection = new SqliteConnection(connectionString))
{
    // Open the connection to the SQLite database file.
    connection.Open();
    // Creates a SqliteCommand object which lets you run SQL commands on this connection.
    var tableCmd = connection.CreateCommand();

    tableCmd.CommandText =
        @"CREATE TABLE IF NOT EXISTS drinking_water (
      Id INTEGER PRIMARY KEY AUTOINCREMENT,
      Date TEXT,
      Quantity INTEGER
    )";

    // ExecuteNonQuery() runs the SQL command but doesn’t return any rows (used for CREATE, INSERT, DELETE, etc.).
    tableCmd.ExecuteNonQuery();
    // Closes the database connection (also handled automatically when the using block ends).
    connection.Close();

    void GetUserInput()
    {
        Console.Clear();
        bool closeApp = false;
        while (closeApp == false)
        {
            Console.WriteLine("\n\nMAIN MENU");
            Console.WriteLine("\nWhat would you like to do?");
            Console.WriteLine("\nType 0 to close application");
            Console.WriteLine("Type 1 to View All Records");
            Console.WriteLine("Type 2 to Insert Record");
            Console.WriteLine("Type 3 to Delete Record");
            Console.WriteLine("Type 4 to Update Record");
            Console.WriteLine("----------------------------------------\n");

            string command = Console.ReadLine();

            switch (command)
            {
                case "0":
                    Console.WriteLine("\nGoodbye!\n");
                    closeApp = true;
                    Environment.Exit(0);
                    break;
                case "1":
                    GetAllRecords();
                    break;
                case "2":
                    Insert();
                    break;
                case "3":
                    Delete();
                    break;
                case "4":
                    Update();
                    break;
                default:
                    Console.WriteLine("\nInvalid Command. Please type a number from 0 to 4.");
                    break;
            }
        }
    }

    void GetAllRecords()
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            var tableCmd = connection.CreateCommand();

            tableCmd.CommandText = $"SELECT * FROM drinking_water";

            List<DrinkingWater> tableData = new List<DrinkingWater>();

            // ExecuteReader() runs the SELECT statement and returns a data reader for reading the results row by row.
            SqliteDataReader reader = tableCmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    tableData.Add(
                        new DrinkingWater
                        {
                            Id = reader.GetInt32(0),
                            Date = DateTime.ParseExact(
                                reader.GetString(1),
                                "dd-MM-yy",
                                new CultureInfo("en-US")
                            ),
                            Quantity = reader.GetInt32(2),
                        }
                    );
                }
            }
            else
            {
                Console.WriteLine("No rows found");
            }

            // tableCmd.ExecuteNonQuery();
            connection.Close();

            Console.WriteLine("------------------------------------------\n");
            foreach (var dw in tableData)
            {
                Console.WriteLine(
                    $"{dw.Id} - {dw.Date.ToString("dd-MMM-yyyy")} - Quantity: {dw.Quantity}"
                );
            }
            Console.WriteLine("------------------------------------------\n");
        }
    }

    void Insert()
    {
        string date = GetDateInput();

        int quantity = GetNumberInput(
            "\n\nPlease insert number of glasses or other measure of your choice (no decimals allowed)\n\n"
        );

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            var tableCmd = connection.CreateCommand();

            // Insert into the DB using Parameterized queries to prevent SQL injection.
            tableCmd.CommandText =
                $"INSERT INTO drinking_water (date, quantity) VALUES (@date, @quantity)";
            tableCmd.Parameters.AddWithValue("@date", date);
            tableCmd.Parameters.AddWithValue("@quantity", quantity);

            tableCmd.ExecuteNonQuery();
            connection.Close();
        }
    }

    string GetDateInput()
    {
        Console.WriteLine(
            "\n\nPlease insert the date: (Format: dd-mm-yy). Type 0 to return to the main menu\n\n"
        );

        string dateInput = Console.ReadLine();

        if (dateInput == "0")
            GetUserInput();

        while (
            !DateTime.TryParseExact(
                dateInput,
                "dd-MM-yy",
                new CultureInfo("en-US"),
                DateTimeStyles.None,
                out _
            )
        )
        {
            Console.WriteLine(
                "\n\nInvalid date. (Format: dd-mm-yy). Type 0 to return to main menu or try again."
            );
            dateInput = Console.ReadLine();
        }

        return dateInput;
    }

    int GetNumberInput(string message)
    {
        Console.WriteLine(message);

        string numberInput = Console.ReadLine();

        if (numberInput == "0")
            GetUserInput();

        while (!Int32.TryParse(numberInput, out _) || Convert.ToInt32(numberInput) < 0)
        {
            Console.WriteLine("\n\nInvalid number. Try again.\n\n");
            numberInput = Console.ReadLine();
        }

        int finalInput = Convert.ToInt32(numberInput);

        return finalInput;
    }

    void Delete()
    {
        Console.Clear();
        GetAllRecords();

        var recordId = GetNumberInput(
            "\n\nPlease type the id of the record you want to delete or type 0 to return to the Main Menu\n\n"
        );

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = $"DELETE FROM drinking_water WHERE Id = '{recordId}'";

            int rowCount = tableCmd.ExecuteNonQuery();

            if (rowCount == 0)
            {
                Console.WriteLine($"\n\nRecord with Id {recordId} doesn't exist.\n\n");
                Delete();
            }
        }

        Console.WriteLine($"\n\nRecord with Id {recordId} was deleted.\n\n");

        GetUserInput();
    }

    void Update()
    {
        Console.Clear();
        GetAllRecords();

        var recordId = GetNumberInput(
            "\n\nPlease type Id of the record you would like to update. Type 0 to return to the main menu.\n\n"
        );

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            var checkCmd = connection.CreateCommand();
            checkCmd.CommandText =
                $"SELECT EXISTS(SELECT 1 FROM drinking_water WHERE Id = {recordId})";
            // Return the value from the database. 0 == false, 1 == true
            int checkQuery = Convert.ToInt32(checkCmd.ExecuteScalar());

            if (checkQuery == 0)
            {
                Console.WriteLine($"\n\nRecord with Id {recordId} doesn't exist.\n\n");
                connection.Close();
                Update();
            }

            string date = GetDateInput();

            int quantity = GetNumberInput(
                "\n\nPlease insert number of glasses or other measure of your choice (no decimals allowed)\n\n"
            );

            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText =
                $"UPDATE drinking_water SET date = '{date}', quantity = {quantity} WHERE Id = {recordId}";

            tableCmd.ExecuteNonQuery();

            connection.Close();
        }
    }

    GetUserInput();
}

public class DrinkingWater
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int Quantity;
}

public class Habit
{
    public string habitName { get; set; }
    public int habitMeasurement { get; set; }
}
