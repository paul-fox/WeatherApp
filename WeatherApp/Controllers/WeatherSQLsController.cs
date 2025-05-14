using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace WeatherApp.Controllers
{
    public class WeatherSQLsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public static void InsertWeather()
        {
            // Replace with your actual connection string
            string connectionString = "Server=YOUR_SERVER_NAME;Database=YOUR_DATABASE_NAME;Trusted_Connection=True;";

            // SQL insert command
            string insertQuery = "INSERT INTO Employees (Id, Name, Position) VALUES (@Id, @Name, @Position)";

            // Sample data to insert
            int id = 1;
            string name = "John Doe";
            string position = "Software Engineer";

            // Using statement ensures connection is closed automatically
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(insertQuery, connection);

                // Add parameters to avoid SQL injection
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Position", position);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"Insert successful. Rows affected: {rowsAffected}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
        }
    }
}
