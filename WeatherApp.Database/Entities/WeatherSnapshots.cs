namespace WeatherApp.DatabaseConnector.Entities
{
    public class WeatherSnapshots
    {
        public int Id { get; set; }

        public string? Country { get; set; }
        public string? City { get; set; }
        public decimal TemperatureC { get; set; }
        public DateTime LastUpdate { get; set; }
        public DateTime SavedOn { get; set; }
    }
}
