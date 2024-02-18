using Microsoft.EntityFrameworkCore;
using WeatherApp.DatabaseConnector.Entities;

namespace WeatherApp.DatabaseConnector
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
            
        }

        public DbSet<CurrentWeather> WeatherForecasts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CurrentWeather>()
                .Property(p => p.TemperatureC)
                .HasPrecision(6, 2);
        }
    }
}
