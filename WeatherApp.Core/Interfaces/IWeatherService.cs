using WeatherApp.Models.Models;

namespace WeatherApp.Core.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherReport> GetCurrentWeatherAsync(string city);
    }    
}
