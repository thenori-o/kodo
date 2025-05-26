using System.Text.Json.Serialization;

namespace ClickUpSdk.DTOs
{
    public class ErrorResponse
    {
        [JsonPropertyName("err")]
        public string Err { get; set; } = string.Empty;

        [JsonPropertyName("ECODE")]
        public string ECode { get; set; } = string.Empty;
    }
}
