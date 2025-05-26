using System.Text.Json.Serialization;

namespace Infrastructure.ClickUp.DTOs
{
    public class ErrorResponse
    {
        [JsonPropertyName("err")]
        public string Err { get; set; } = string.Empty;

        [JsonPropertyName("ECODE")]
        public string ECode { get; set; } = string.Empty;
    }
}
