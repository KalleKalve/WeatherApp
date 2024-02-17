using Microsoft.AspNetCore.Mvc;
using WeatherApp.Models.Graph;

namespace WeatherApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GraphDataController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<GraphData> Get()
        {
            // Fetch and return data from the database
            return new List<GraphData> {
                // Example data
                new GraphData { Country = "Latvia", City = "Riga", TemperatureC = 12.2m, LastUpdated = new DateTime(2023, 02, 02, 10, 01, 00) },
                new GraphData { Country = "Latvia", City = "Riga", TemperatureC = 13.3m, LastUpdated = new DateTime(2023, 02, 02, 10, 02, 00) },
                new GraphData { Country = "Latvia", City = "Jēkabpils", TemperatureC = 14.4m, LastUpdated = new DateTime(2023, 02, 02, 10, 01, 00) },
                new GraphData { Country = "Latvia", City = "Jelgava", TemperatureC = 12.2m, LastUpdated = new DateTime(2023, 02, 02, 10, 01, 00) },
                new GraphData { Country = "Latvia", City = "Riga", TemperatureC = 12.2m, LastUpdated = new DateTime(2023, 02, 02, 10, 03, 00) },
                new GraphData { Country = "Estonia", City = "Tallinn", TemperatureC = 10.2m, LastUpdated = new DateTime(2023, 02, 02, 10, 01, 00) },
                new GraphData { Country = "Estonia", City = "Kuressaare", TemperatureC = 11.2m, LastUpdated = new DateTime(2023, 02, 02, 10, 01, 00) },
                new GraphData { Country = "Estonia", City = "Kuressaare", TemperatureC = 11.2m, LastUpdated = new DateTime(2023, 02, 02, 10, 01, 00) },
            };
        }        
    }
}
