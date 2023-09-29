using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ParsecSharp
{
    public static class Api
    {
        private const string BaseAddress = "https://kessel-api.parsecgaming.com";
    
        private static readonly HttpClient Client = new HttpClient();
    
        private static async ValueTask<HttpResponseMessage> Get([NotNull] string path, string query = null, IDictionary<string, string> headers = null)
        {
            var uri = BaseAddress + path;
            if (query != null)
            {
                uri += "?" + query;
            }
        
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            if (headers != null)
            {
                foreach (var (key, value) in headers)
                {
                    request.Headers.Add(key, value);
                }
            }
            return await Client.SendAsync(request);
        }
    
        private static async ValueTask<HttpResponseMessage> Post([NotNull] string path, object body = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, BaseAddress + path);
            if (body != null)
            {
                request.Content = new StringContent(
                    JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
            }
        
            return await Client.SendAsync(request);
        }

        #region Auth

        public static async ValueTask<AuthResult> AuthPersonal(AuthPersonalCredentials credentials)
        {
            var response = await Post("/v1/auth", credentials);
        
            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<AuthErrorBody>(await response.Content.ReadAsStringAsync()) ??
                            throw new HttpRequestException();

                if (error.TfaRequired is true)
                {
                    throw new InvalidCredentialException("TFA required");
                }
            
                throw new InvalidCredentialException(error.Error);
            }
        
            return JsonConvert.DeserializeObject<AuthResult>(await response.Content.ReadAsStringAsync()) ??
                   throw new HttpRequestException();
        }
    
        [Serializable]
        public class AuthPersonalCredentials
        {
            /// <summary>
            /// Parsec account's email address
            /// </summary>
            [NotNull]
            [JsonRequired]
            [JsonProperty("email")]
            public string Email { get; set; }
        
            /// <summary>
            /// Parsec account's password
            /// </summary>
            [NotNull]
            [JsonRequired]
            [JsonProperty("password")]
            public string Password { get; set; }
        
            /// <summary>
            /// Optional TFA code
            /// </summary>
            [JsonProperty("tfa")]
            public string Tfa { get; set; }
        }
    
        [Serializable]
        public class AuthResult
        {
            [NotNull]
            [JsonRequired]
            [JsonProperty("instance_id")]
            public string InstanceId { get; set; }
        
            [JsonRequired]
            [JsonProperty("user_id")]
            public int UserId { get; set; }
        
            [NotNull]
            [JsonRequired]
            [JsonProperty("session_id")]
            public string SessionId { get; set; }
        
            [NotNull]
            [JsonRequired]
            [JsonProperty("host_peer_id")]
            public string HostPeerId { get; set; }
        }

        [Serializable]
        public class AuthErrorBody
        {
            [NotNull]
            [JsonRequired]
            [JsonProperty("error")]
            public string Error { get; set; }
        
            [JsonProperty("tfa_required")]
            public bool? TfaRequired { get; set; }
        
            [JsonProperty("tfa_type")]
            public string TfaType { get; set; }
        }

        #endregion

        #region GetHosts
    
        public static async ValueTask<GetHostsResult> GetHosts(GetHostsQueryParams queryParams)
        {
            var query = new StringBuilder();
            string mode;
            switch (queryParams.Mode)
            {
                case HostMode.Desktop:
                    mode = "desktop";
                    break;
                case HostMode.Game:
                    mode = "game";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(queryParams.Mode));
            }
            query.Append("mode=").Append(mode);
            if (queryParams.IsPublic != null)
            {
                query.Append("&public=");
                query.Append(queryParams.IsPublic.Value);
            }
        
            var response = await Get("/v2/hosts", query.ToString(), new Dictionary<string, string>
            {
                { "Authorization", "Bearer " + queryParams.SessionId },
            });
            response.EnsureSuccessStatusCode();
        
            return JsonConvert.DeserializeObject<GetHostsResult>(await response.Content.ReadAsStringAsync()) ??
                   throw new HttpRequestException();
        }

        public enum HostMode
        {
            Desktop,
            Game
        }
    
        private class HostModeJsonConverter : JsonConverter<HostMode>
        {
            public override void WriteJson(JsonWriter writer, HostMode value, JsonSerializer serializer)
            {
                string hostMode;
                switch (value)
                {
                    case HostMode.Desktop:
                        hostMode = "desktop";
                        break;
                    case HostMode.Game:
                        hostMode = "game";
                        break;
                    default:
                        throw new ArgumentException($"Unexpected value {value}");
                }
                writer.WriteValue(hostMode);
            }

            public override HostMode ReadJson(JsonReader reader, Type objectType, HostMode existingValue, bool hasExistingValue, JsonSerializer serializer)
            {
                switch (reader.Value)
                {
                    case "desktop":
                        return HostMode.Desktop;
                    case "game":
                        return HostMode.Game;
                    default:
                        throw new JsonException();
                }
            }
        }

        [Serializable]
        public class GetHostsQueryParams
        {
            [NotNull]
            public string SessionId { get; set; }
        
            public HostMode Mode { get; set; }
        
            public bool? IsPublic { get; set; }
        }
    
        [Serializable]
        public class User {
            [JsonRequired]
            [JsonProperty("id")]
            public int Id { get; set; }
        
            [NotNull]
            [JsonRequired]
            [JsonProperty("name")]
            public string Name { get; set; }
        
            [JsonRequired]
            [JsonProperty("warp")]
            public bool Warp { get; set; }
        }
    
        [Serializable]
        public class Host {
            /// <summary>
            /// Host computer's peer ID
            /// </summary>
            [NotNull]
            [JsonRequired]
            [JsonProperty("peer_id")]
            public string PeerId { get; set; }
        
            /// <summary>
            /// User that created the host
            /// </summary>
            [NotNull]
            [JsonRequired]
            [JsonProperty("user")]
            public User User { get; set; }
        
            /// <summary>
            /// Internal Parsec game ID
            /// </summary>
            [NotNull]
            [JsonRequired]
            [JsonProperty("game_id")]
            public string GameId { get; set; }
        
            /// <summary>
            /// Parsec build number
            /// </summary>
            [NotNull]
            [JsonRequired]
            [JsonProperty("build")]
            public string Build { get; set; }
        
            /// <summary>
            /// Host's description
            /// </summary>
            [NotNull]
            [JsonRequired]
            [JsonProperty("description")]
            public string Description { get; set; }
        
            /// <summary>
            /// Maximal number of players allowed to be connected simultaneously
            /// </summary>
            [JsonRequired]
            [JsonProperty("max_players")]
            public int MaxPlayers { get; set; }
        
            /// <summary>
            /// Host's mode
            /// </summary>
            [JsonRequired]
            [JsonProperty("mode")]
            [JsonConverter(typeof(HostModeJsonConverter))]
            public HostMode Mode { get; set; }
        
            [NotNull]
            [JsonRequired]
            [JsonProperty("name")]
            public string Name { get; set; }
        
            /// <summary>
            /// Number of players currently connected to the host
            /// </summary>
            [JsonRequired]
            [JsonProperty("players")]
            public int Players { get; set; }
        
            /// <summary>
            /// Host's visibility
            /// </summary>
            [JsonRequired]
            [JsonProperty("public")]
            public bool IsPublic { get; set; }
        
            /// <summary>
            /// Determines if the host that made the `GET /hosts` call is attached to the same sessionID
            /// </summary>
            [JsonRequired]
            [JsonProperty("self")]
            public bool IsSelf { get; set; }
        }
    
        [Serializable]
        public class GetHostsResult
        {
            [NotNull]
            [JsonRequired]
            [JsonProperty("data")]
            public Host[] Data { get; set; }
        
            [JsonRequired]
            [JsonProperty("has_more")]
            public bool HasMore { get; set; }
        }

        #endregion
    }
}