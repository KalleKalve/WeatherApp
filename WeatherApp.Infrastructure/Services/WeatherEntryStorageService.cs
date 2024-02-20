using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using WeatherApp.Core.Interfaces;
using WeatherApp.DatabaseConnector;
using WeatherApp.Models.Models;

namespace WeatherApp.Infrastructure.Services
{
    public class WeatherEntryStorageService : IWeatherEntryStorageService
    {
        private readonly ApplicationDbContext _context;

        public WeatherEntryStorageService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<WeatherReport>> GetEntriesForGraphByListOfCities(List<string> cityNames)
        {
            var maxTemps = await _context.WeatherSnapshots
                .Where(snapshot => cityNames.Contains(snapshot.City))
                .GroupBy(snapshot => snapshot.City)
                .Select(g => g.OrderByDescending(s => s.TemperatureC).FirstOrDefault())
                .ToListAsync();

            var minTemps = await _context.WeatherSnapshots
                .Where(snapshot => cityNames.Contains(snapshot.City))
                .GroupBy(snapshot => snapshot.City)
                .Select(g => g.OrderBy(s => s.TemperatureC).FirstOrDefault())
                .ToListAsync();

            var combinedResults = maxTemps.Union(minTemps).ToList();

            var validEntries = combinedResults.Where(entry => entry != null).ToList();

            var reports = validEntries.Select(snapshot => new WeatherReport
            {
                Country = snapshot.Country,
                City = snapshot.City,
                TemperatureC = snapshot.TemperatureC,
                LastUpdated = snapshot.LastUpdate
            }).ToList();

         return reports;
        }
    }
}
