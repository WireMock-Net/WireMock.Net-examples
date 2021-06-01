using System.Threading.Tasks;
using OpenWeather.Business;

namespace OpenWeather.ConsoleVersion1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new OpenWeatherClient("https://api.openweathermap.org/data/2.5", "3141d5a3756312d1295eefeadbccc24d");

            var service = new OpenWeatherService(client);

            var result = await service.GetInfoAsync("Sittard, NL");

            System.Console.WriteLine($"{result.DegreesCelsius:##.##} celsius ({result.Description})");
        }
    }
}
