using Microsoft.EntityFrameworkCore;
using WeatherApp.Core.Interfaces;
using WeatherApp.DatabaseConnector;
using WeatherApp.DatabaseConnector.Entities;

namespace WeatherApp.Infrastructure.Services
{
    public class DataFetchService : IDataFetchService
    {
        private readonly IWeatherService _weatherService;
        private readonly ApplicationDbContext _dbContext;

        public DataFetchService(IWeatherService weatherService, ApplicationDbContext dbContext)
        {
            _weatherService = weatherService;
            _dbContext = dbContext;
        }

        public async Task FetchAndSaveDataAsync(string cityName)
        {
            var data = await _weatherService.GetCurrentWeatherAsync(cityName).ConfigureAwait(false);

            bool exists = await _dbContext.WeatherSnapshots.AnyAsync(snapshot =>
                snapshot.City == data.City &&
                snapshot.Country == data.Country &&
                snapshot.TemperatureC == data.TemperatureC &&
                snapshot.LastUpdate == data.LastUpdated)
                .ConfigureAwait(false);

            if (!exists)
            {
                await _dbContext.WeatherSnapshots.AddAsync(new WeatherSnapshots
                {
                    City = data.City,
                    Country = data.Country,
                    TemperatureC = data.TemperatureC,
                    LastUpdate = data.LastUpdated,
                    SavedOn = DateTime.UtcNow
                })
                .ConfigureAwait(false);

                await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
        }
    }
}
