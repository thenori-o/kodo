using System.Text.Json.Serialization;

namespace ClickUpClient.ClickUp.DTOs
{
    public class AccessTokenResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; } = string.Empty;
    }
}
