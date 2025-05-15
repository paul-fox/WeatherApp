using Microsoft.EntityFrameworkCore;
using WeatherApp.Models;

namespace WeatherApp.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> contextOptions)
            : base(contextOptions)
        {

        }

        //public DbSet<LocationSQL> LocationsSQL { get; set; }
        public DbSet<WeatherSqlModel> Weathers { get; set; }
        //public DbSet<LocationAPI> LocationsAPI { get; set; }
        //public DbSet<WeatherAPI.Root> WeathersAPI { get; set; }
    }
}
