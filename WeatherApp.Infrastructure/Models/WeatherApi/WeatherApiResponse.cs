namespace WeatherApp.Infrastructure.Models.WeatherApi
{
    public  class WeatherApiResponse
    {
        public Location location { get; set; }
        public Current current { get; set; }
    }
}
