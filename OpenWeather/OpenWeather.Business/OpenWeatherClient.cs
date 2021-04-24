using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using OpenWeather.Business.Models;

namespace OpenWeather.Business
{
    public class OpenWeatherClient
    {
        private readonly string _url;
        private readonly string _key;
        private readonly HttpClient _client = new HttpClient();

        public OpenWeatherClient(string url, string key)
        {
            _url = url;
            _key = key;
        }

        public Task<OpenWeatherResponse> GetDataAsync(string city)
        {
            var url = $"{_url}/weather?q={city}&appid={_key}";
            return _client.GetFromJsonAsync<OpenWeatherResponse>(url);
        }
    }
}