namespace Application.UseCases.GetWebhooks
{
    public record GetWebhooksInput(long TeamId);

    public record GetWebhooksOutput(IEnumerable<WebHookInfo> Webhooks);
}
