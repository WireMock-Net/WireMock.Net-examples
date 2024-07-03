using System;
using System.Threading.Tasks;
using Microsoft.Graph;
using Microsoft.Kiota.Abstractions.Authentication;
using WireMock.Logging;
using WireMock.Server;
using WireMock.Settings;
using WireMockRequest = WireMock.RequestBuilders.Request;
using WireMockResponse = WireMock.ResponseBuilders.Response;

namespace MicrosoftGraphConsole;

class Program
{
    static async Task Main(string[] args)
    {
        var server = WireMockServer.Start(new WireMockServerSettings
        {
            Logger = new WireMockConsoleLogger()
        });

        var data = new
        {
            odata = new
            {
                context = "https://graph.microsoft.com/v1.0/$metadata#users",
                count = 2,
                nextLink = (string?)null
            },
            value = new[]
            {
                new
                {
                    displayName = "Conf Room Adams",
                    jobTitle = "Test",
                    mail = "Adams@contoso.com",
                    userPrincipalName = "Adams@contoso.com",
                    id = "6ea91a8d-e32e-41a1-b7bd-d2d185eed0e0"
                },
                new
                {
                    displayName = "MOD Administrator",
                    jobTitle = "Admin",
                    mail = "admin@contoso.com",
                    userPrincipalName = "admin@contoso.com",
                    id = "4562bcc8-c436-4f95-b7c0-4f8ce89dca5e"
                }
            }
        };

        server
            .Given(WireMockRequest.Create()
                .WithPath("/users")
                .UsingGet()
            )
            .RespondWith(WireMockResponse.Create()
                .WithBodyAsJson(data)
                .WithHeader("content-type", "application/json")
            );

        var graphClient = new GraphServiceClient(server.CreateClient(), new AnonymousAuthenticationProvider());

        var result = await graphClient.Users.GetAsync();

        Console.WriteLine(new string('*', 80));
        foreach (var user in result!.Value!)
        {
            Console.WriteLine(user.Id);
            Console.WriteLine(user.DisplayName);
            Console.WriteLine(user.JobTitle);
            Console.WriteLine(user.Mail);
            Console.WriteLine(user.UserPrincipalName);
            Console.WriteLine(new string('-', 20));
        }
    }
}