namespace Infrastructure.ClickUp.DTOs
{
    public class GetWebhookResponse
    {
        public List<WebHookInfo> Webhooks { get; set; } = new();
    }
}
