using System.Security.Authentication;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ParsecSharp;

public static class Api
{
    public const string BaseAddress = "https://kessel-api.parsecgaming.com";
    
    private static readonly HttpClient Client = new();
    
    private static async ValueTask<HttpResponseMessage> Get(string path, string? query = null)
    {
        var uri = BaseAddress + path;
        if (query is not null)
        {
            uri += "?" + query;
        }
        
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        return await Client.SendAsync(request);
    }
    
    private static async ValueTask<HttpResponseMessage> Post(string path, object? body = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, BaseAddress + path);
        if (body is not null)
        {
            request.Content = new StringContent(
                JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
        }
        
        return await Client.SendAsync(request);
    }

    #region Auth

    public static async ValueTask<AuthResult> AuthPersonal(AuthPersonalCredentials credentials)
    {
        var response = await Post("/v1/auth", credentials);
        
        if (!response.IsSuccessStatusCode)
        {
            var error = JsonSerializer.Deserialize<AuthErrorBody>(await response.Content.ReadAsStringAsync()) ??
                        throw new HttpRequestException();

            if (error.TfaRequired is true)
            {
                throw new InvalidCredentialException("TFA required");
            }
            
            throw new InvalidCredentialException(error.Error);
        }
        
        return JsonSerializer.Deserialize<AuthResult>(await response.Content.ReadAsStringAsync()) ??
               throw new HttpRequestException();
    }
    
    public class AuthPersonalCredentials
    {
        /// <summary>
        /// Parsec account's email address
        /// </summary>
        [JsonPropertyName("email")]
        public required string Email { get; init; }
        
        /// <summary>
        /// Parsec account's password
        /// </summary>
        [JsonPropertyName("password")]
        public required string Password { get; init; }
        
        /// <summary>
        /// Optional TFA code
        /// </summary>
        [JsonPropertyName("tfa")]
        public string? Tfa { get; init; }
    }
    
    public class AuthResult
    {
        [JsonPropertyName("instance_id")]
        public required string InstanceId { get; init; }
        
        [JsonPropertyName("user_id")]
        public required int UserId { get; init; }
        
        [JsonPropertyName("session_id")]
        public required string SessionId { get; init; }
        
        [JsonPropertyName("host_peer_id")]
        public required string HostPeerId { get; init; }
    }

    public class AuthErrorBody
    {
        [JsonPropertyName("error")]
        public required string Error { get; init; }
        
        [JsonPropertyName("tfa_required")]
        public bool? TfaRequired { get; init; }
        
        [JsonPropertyName("tfa_type")]
        public string? TfaType { get; init; }
    }

    #endregion

    #region GetHosts
    
    public static async ValueTask<GetHostsResult> GetHosts(GetHostsQueryParams queryParams)
    {
        var query = JsonSerializer.Serialize(queryParams, new JsonSerializerOptions
        {
            Converters =
            {
                new HostModeJsonConverter()
            }
        });
        var response = await Get("/v1/hosts", query);
        response.EnsureSuccessStatusCode();
        
        return JsonSerializer.Deserialize<GetHostsResult>(await response.Content.ReadAsStringAsync()) ??
               throw new HttpRequestException();
    }

    public enum HostMode
    {
        Desktop,
        Game
    }
    
    private class HostModeJsonConverter : JsonConverter<HostMode>
    {
        public override HostMode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.GetString() switch
            {
                "desktop" => HostMode.Desktop,
                "game" => HostMode.Game,
                _ => throw new JsonException()
            };
        }

        public override void Write(Utf8JsonWriter writer, HostMode value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value switch
            {
                HostMode.Desktop => "desktop",
                HostMode.Game => "game",
                _ => throw new ArgumentException($"Unexpected value {value}")
            });
        }
    }

    public class GetHostsQueryParams
    {
        [JsonPropertyName("mode")]
        public HostMode Mode { get; init; }
        
        [JsonPropertyName("public")]
        public bool? IsPublic { get; init; }
    }
    
    public class User {
        [JsonPropertyName("id")]
        public required int Id { get; init; }
        
        [JsonPropertyName("name")]
        public required string Name { get; init; }
        
        [JsonPropertyName("warp")]
        public required bool Warp { get; init; }
    }
    
    public class Host {
        /// <summary>
        /// Host computer's peer ID
        /// </summary>
        [JsonPropertyName("peer_id")]
        public required string PeerId { get; init; }
        
        /// <summary>
        /// User that created the host
        /// </summary>
        [JsonPropertyName("user")]
        public required User User { get; init; }
        
        /// <summary>
        /// Internal Parsec game ID
        /// </summary>
        [JsonPropertyName("game_id")]
        public required string GameId { get; init; }
        
        /// <summary>
        /// Parsec build number
        /// </summary>
        [JsonPropertyName("build")]
        public required string Build { get; init; }
        
        /// <summary>
        /// Host's description
        /// </summary>
        [JsonPropertyName("description")]
        public required string Description { get; init; }
        
        /// <summary>
        /// Maximal number of players allowed to be connected simultaneously
        /// </summary>
        [JsonPropertyName("max_players")]
        public required int MaxPlayers { get; init; }
        
        /// <summary>
        /// Host's mode
        /// </summary>
        [JsonPropertyName("mode")]
        [JsonConverter(typeof(HostModeJsonConverter))]
        public required HostMode Mode { get; init; }
        
        [JsonPropertyName("name")]
        public required string Name { get; init; }
        
        /// <summary>
        /// Number of players currently connected to the host
        /// </summary>
        [JsonPropertyName("players")]
        public required int Players { get; init; }
        
        /// <summary>
        /// Host's visibility
        /// </summary>
        [JsonPropertyName("public")]
        public required bool IsPublic { get; init; }
        
        /// <summary>
        /// Determines if the host that made the `GET /hosts` call is attached to the same sessionID
        /// </summary>
        [JsonPropertyName("self")]
        public required bool IsSelf { get; init; }
    }
    
    public class GetHostsResult
    {
        [JsonPropertyName("data")]
        public required Host[] Data { get; init; }
        
        [JsonPropertyName("has_more")]
        public required bool HasMore { get; init; }
    }

    #endregion
}