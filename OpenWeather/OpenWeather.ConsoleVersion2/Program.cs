using System;
using System.Threading.Tasks;
using OpenWeather.Business;

namespace OpenWeather.ConsoleVersion2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new OpenWeatherClient("https://wiremock.azurewebsites.net", "xxx");

            var service = new OpenWeatherService(client);

            var result = await service.GetInfoAsync("Sittard, NL");

            Console.WriteLine($"{result.DegreesCelsius:##.##} celsius ({result.Description})");
        }
    }
}
