namespace WeatherApp.Core.Interfaces
{
    public interface IDataFetchService
    {
        Task FetchAndSaveDataAsync(string cityName);
    }
}
