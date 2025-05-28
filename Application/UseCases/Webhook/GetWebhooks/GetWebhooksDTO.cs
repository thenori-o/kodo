namespace Application.UseCases.Webhook.GetWebhooks
{
    public record GetWebhooksInput(long TeamId);

    public record GetWebhooksOutput(IEnumerable<WebHookInfo> Webhooks);
}
