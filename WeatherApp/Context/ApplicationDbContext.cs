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

        public DbSet<WeatherSqlModel> WeatherEntries { get; set; }
    }
}
