namespace WeatherApp.Models.Models
{
    public class WeatherReport
    {
        public string? Country { get; set; }
        public string? City { get; set; }
        public decimal TemperatureC { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
