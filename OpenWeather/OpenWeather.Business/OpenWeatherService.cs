using System.Threading.Tasks;
using OpenWeather.Business.Models;

namespace OpenWeather.Business
{
    public class OpenWeatherService
    {
        private readonly OpenWeatherClient _client;

        public OpenWeatherService(OpenWeatherClient client)
        {
            _client = client;
        }

        public async Task<Result> GetInfoAsync(string city)
        {
            var data = await _client.GetDataAsync(city);

            var degreesCelsius = data.Main.Temp - Constants.KelvinZero;

            return new Result
            {
                DegreesCelsius = degreesCelsius,
                Description = GetDescription(degreesCelsius)
            };
        }

        private static string GetDescription(double degreesCelsius)
        {
            if (degreesCelsius < 0)
            {
                return "koud";
            }

            if (degreesCelsius < 10)
            {
                return "fris";
            }

            if (degreesCelsius < 20)
            {
                return "normaal";
            }

            return "warm";
        }
    }
}