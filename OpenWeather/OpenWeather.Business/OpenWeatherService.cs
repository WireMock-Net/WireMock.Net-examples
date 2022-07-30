using System.Threading.Tasks;
using OpenWeather.Business.Models;

namespace OpenWeather.Business;

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
        return degreesCelsius switch
        {
            < 0 => "koud",

            < 10 => "fris",

            < 20 => "normaal",

            _ => "warm"
        };
    }
}