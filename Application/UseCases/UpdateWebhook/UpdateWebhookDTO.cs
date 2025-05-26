namespace Application.UseCases.UpdateWebhook
{
    public record UpdateWebhookInput(string Endpoint, List<string> Events, string status);

    public record UpdateWebhookOutput(
        Guid Id,
        WebHookInfo Webhook
    );
}
