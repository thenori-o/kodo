using System.Text.Json.Serialization;

namespace ClickUpClient.ClickUp.DTOs.Webhook
{
    public class UpdateWebhookRequest
    {
        [JsonPropertyName("webhook_id")]
        public required Guid WebhookId { get; set; }
        public required string Endpoint { get; set; }
        public required string Status { get; set; }
        public List<string> Events { get; set; } = new();

    }

    public class UpdateWebhookResponse
    {
        public required Guid Id { get; set; }
        public required WebHookInfo Webhook { get; set; }
    }
}
