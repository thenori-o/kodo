using System.Text.Json.Serialization;

namespace Infrastructure.ClickUp.DTOs
{

    public class CreateWebhookRequest
    {
        [JsonPropertyName("endpoint")]
        public string Endpoint { get; set; } = string.Empty;

        [JsonPropertyName("team_id")]
        public long? TeamId { get; set; }

        [JsonPropertyName("events")]
        public List<string> Events { get; set; } = new();

        [JsonPropertyName("space_id")]
        public long? SpaceId { get; set; }

        [JsonPropertyName("folder_id")]
        public long? FolderId { get; set; }

        [JsonPropertyName("task_id")]
        public string? TaskId { get; set; }

        [JsonPropertyName("list_id")]
        public long? ListId { get; set; }
    }

    public class CreateWebhookResponse
    {
        public Guid Id { get; set; }
        public WebHookInfoWithViewId Webhook { get; set; } = new();
    }

}
