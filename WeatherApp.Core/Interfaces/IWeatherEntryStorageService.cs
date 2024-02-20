using WeatherApp.Models.Models;

namespace WeatherApp.Core.Interfaces
{
    public interface IWeatherEntryStorageService
    {
        Task<List<WeatherReport>> GetEntriesForGraphByListOfCities(List<string> cityNames);
    }
}
