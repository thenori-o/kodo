namespace ClickUpClient.ClickUp.DTOs.Webhook
{
    public class GetWebhookResponse
    {
        public List<WebHookInfo> Webhooks { get; set; } = new();
    }
}
