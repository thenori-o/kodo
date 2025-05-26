using System.Text.Json.Serialization;

namespace ClickUpSdk.DTOs
{
    public class AccessTokenResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; } = string.Empty;
    }
}
