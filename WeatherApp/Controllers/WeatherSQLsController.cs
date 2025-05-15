using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.Controllers
{
    public class WeatherSQLsController : BaseController
    {
        public WeatherSQLsController(
            ILogger<WeatherSQLsController> logger,
            IOptions<MySettingsModel> mySettings,
            ILocationApiService locationService,
            IWeatherApiService weatherService)
            : base(logger, mySettings, locationService, weatherService)
        {
        }

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
