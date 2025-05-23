using System.Text.Json.Serialization;

namespace ClickUpClient.ClickUp.DTOs.Webhook
{
    public class WebHookInfo
    {
        public Guid Id { get; set; }
        public long UserId { get; set; }

        [JsonPropertyName("team_id")]
        public long TeamId { get; set; }
        public string Endpoint { get; set; } = string.Empty;

        [JsonPropertyName("client_id")]
        public string ClientId { get; set; } = string.Empty;
        public List<string> Events { get; set; } = new();

        [JsonPropertyName("task_id")]
        public string? TaskId { get; set; }

        [JsonPropertyName("list_id")]
        public long? ListId { get; set; }

        [JsonPropertyName("folder_id")]
        public long? FolderId { get; set; }

        [JsonPropertyName("space_id")]
        public long? SpaceId { get; set; }
        public WebhookHealth Health { get; set; } = new();
        public string Secret { get; set; } = string.Empty;
    }

    public class WebHookInfoWithViewId : WebHookInfo
    {
        [JsonPropertyName("view_id")]
        public string? ViewId { get; set; }
    }

    public class WebhookHealth
    {
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("fail_count")]
        public int FailCount { get; set; }
    }
}
