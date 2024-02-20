using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using WeatherApp.DatabaseConnector.Entities;

namespace WeatherApp.DatabaseConnector
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
            
        }

        public DbSet<WeatherSnapshots> WeatherSnapshots { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeatherSnapshots>()
                .Property(p => p.TemperatureC)
                .HasPrecision(6, 2);
            
            modelBuilder.Entity<WeatherSnapshots>().HasData(new WeatherSnapshots
            {
                Id = 1, 
                Country = "Latvia",
                City = "Riga",
                TemperatureC = 1.5m,
                LastUpdate = new DateTime(2024, 2, 20, 10, 50, 0),
                SavedOn = DateTime.UtcNow
            },
            new WeatherSnapshots 
            {
                Id = 2,
                Country = "Estonia",
                City = "Tallinn",
                TemperatureC = 1.0m,
                LastUpdate = new DateTime(2024, 2, 20, 10, 50, 0),
                SavedOn = DateTime.UtcNow
            },
            new WeatherSnapshots
            {
                Id = 3,
                Country = "Latvia",
                City = "Riga",
                TemperatureC = -0.5m,
                LastUpdate = new DateTime(2024, 2, 20, 11, 0, 0),
                SavedOn = DateTime.UtcNow + TimeSpan.FromMinutes(10)
            },
            new WeatherSnapshots
            {
                Id = 4,
                Country = "Estonia",
                City = "Tallinn",
                TemperatureC = -1.0m,
                LastUpdate = new DateTime(2024, 2, 20, 11, 0, 0),
                SavedOn = DateTime.UtcNow + TimeSpan.FromMinutes(10)
            });
        }
    }
}
