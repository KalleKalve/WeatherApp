using Microsoft.AspNetCore.Mvc;
using WeatherApp.Core.Interfaces;
using WeatherApp.MVC.Services;

namespace WeatherApp.MVC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GraphDataController : ControllerBase
    {
        private readonly DefaultQueryValues _defaultQueryValues;
        private readonly IWeatherEntryStorageService _graphService;
        public GraphDataController(DefaultQueryValues defaultQueryValues, IWeatherEntryStorageService graphService)
        {
            _defaultQueryValues = defaultQueryValues;
            _graphService = graphService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _graphService.GetEntriesForGraphByListOfCities(_defaultQueryValues.CityNames);

            if (data == null || !data.Any())
                return NotFound();

            return Ok(data);

        }        
    }
}
