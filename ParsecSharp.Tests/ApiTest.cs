using Newtonsoft.Json;

namespace ParsecSharp.Tests;

public class ApiTest
{
    [SetUp]
    public void Setup()
    { }

    public async static ValueTask<Api.AuthResult> Auth()
    {
        const string credentialsFile = "./credentials.json";
        if (!File.Exists(credentialsFile))
        {
            Assert.Ignore("Credentials file not found");
        }
        
        var credentials = JsonConvert.DeserializeObject<Api.AuthPersonalCredentials>(await File.ReadAllTextAsync(credentialsFile)) ??
                          throw new Exception("Failed to deserialize credentials");
        return await Api.AuthPersonal(credentials);
    }

    [Test]
    public async ValueTask TestAuth()
    {
        var result = await Auth();
        Console.WriteLine(JsonConvert.SerializeObject(result));
    }
    
    [Test]
    public async Task TestGetHosts()
    {
        var authResult = await Auth();
        var hosts = await Api.GetHosts(new Api.GetHostsQueryParams
        {
            SessionId = authResult.SessionId,
            Mode = Api.HostMode.Desktop
        });
        Console.WriteLine(JsonConvert.SerializeObject(hosts));
    }
}