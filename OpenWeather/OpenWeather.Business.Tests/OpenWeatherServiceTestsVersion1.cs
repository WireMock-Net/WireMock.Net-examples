using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace OpenWeather.Business.Tests
{
    public class OpenWeatherServiceTestsVersion1
    {
        private readonly OpenWeatherService _sut;

        public OpenWeatherServiceTestsVersion1()
        {
            var client = new OpenWeatherClient("https://api.openweathermap.org/data/2.5/", "3141d5a3756312d1295eefeadbccc24d");
            _sut = new OpenWeatherService(client);
        }

        [Fact]
        public async Task GetInfoAsync_For_A_City_Should_Return_Correct_Result()
        {
            // Arrange

            // Act
            var result = await _sut.GetInfoAsync("Sittard, NL");

            // Assert
            result.DegreesCelsius.Should().Be(11);
            result.Description.Should().Be("normaal");
        }
    }
}