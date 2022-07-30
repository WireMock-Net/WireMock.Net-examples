using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using Xunit;

namespace OpenWeather.Business.Tests;

public class OpenWeatherServiceTestsVersion2
{
    private readonly WireMockServer _server;
    private readonly OpenWeatherService _sut;

    public OpenWeatherServiceTestsVersion2()
    {
        _server = WireMockServer.Start();

        var client = new OpenWeatherClient(_server.Url, "any string");
        
        _sut = new OpenWeatherService(client);
    }

    [Fact]
    public async Task GetInfoAsync_For_A_City_Should_Return_Correct_Result()
    {
        // Arrange
        var data =
            "{\"coord\":{\"lon\":5.8694,\"lat\":50.9983},\"weather\":[{\"id\":804,\"main\":\"Clouds\",\"description\":\"overcast clouds\",\"icon\":\"04d\"}],\"base\":\"stations\",\"main\":{\"temp\":284.15,\"feels_like\":283.96,\"temp_min\":284.26,\"temp_max\":286.48,\"pressure\":1026,\"humidity\":50},\"visibility\":10000,\"wind\":{\"speed\":5.14,\"deg\":60},\"clouds\":{\"all\":95},\"dt\":1619266365,\"sys\":{\"type\":1,\"id\":1525,\"country\":\"NL\",\"sunrise\":1619238177,\"sunset\":1619289959},\"timezone\":7200,\"id\":2747203,\"name\":\"Sittard\",\"cod\":200}";

        _server
            .Given(
                Request.Create().WithPath("/weather").UsingGet()
            )
            .RespondWith(
                Response.Create()
                    .WithStatusCode(200)
                    .WithHeader("Content-Type", "application/json; charset=utf-8")
                    .WithBody(data)
            );

        // Act
        var result = await _sut.GetInfoAsync("Sittard, NL");

        // Assert
        result.DegreesCelsius.Should().Be(11);
        result.Description.Should().Be("normaal");
    }
}