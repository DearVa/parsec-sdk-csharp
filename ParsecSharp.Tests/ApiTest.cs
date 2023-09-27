using System.Text.Json;

namespace ParsecSharp.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    { }

    private async static ValueTask<Api.AuthResult> Auth()
    {
        const string credentialsFile = "./credentials.json";
        if (!File.Exists(credentialsFile))
        {
            Assert.Ignore("Credentials file not found");
        }
        
        var credentials = JsonSerializer.Deserialize<Api.AuthPersonalCredentials>(await File.ReadAllTextAsync(credentialsFile)) ??
                          throw new Exception("Failed to deserialize credentials");
        return await Api.AuthPersonal(credentials);
    }

    [Test]
    public async ValueTask TestAuth()
    {
        var result = await Auth();
        Console.WriteLine(JsonSerializer.Serialize(result));
    }
    
    [Test]
    public async Task TestGetHosts()
    {
        var authResult = await Auth();
        var result = await Api.GetHosts(new Api.GetHostsQueryParams
        {
            SessionId = authResult.SessionId,
            Mode = Api.HostMode.Desktop
        });
        Console.WriteLine(JsonSerializer.Serialize(result));
    }
}