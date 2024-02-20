using System.Text.Json;
using WeatherApp.Core.Interfaces;
using WeatherApp.Infrastructure.Models.WeatherApi;
using WeatherApp.Models.Models;

namespace WeatherApp.Infrastructure.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherReport> GetCurrentWeatherAsync(string city)
        {
            var response = await _httpClient.GetAsync($"?q={city}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var deserializedRsult = JsonSerializer.Deserialize<WeatherApiResponse>(content);

            if (deserializedRsult != null)
            {
                return new WeatherReport
                {
                    Country = deserializedRsult.location.country,
                    City = deserializedRsult.location.name,
                    TemperatureC = deserializedRsult.current.temp_c,
                    LastUpdated = DateTime.Parse(deserializedRsult.current.last_updated)
                };
            }

            throw new InvalidOperationException("Failed to deserialize weather data.");
        }
    }
}