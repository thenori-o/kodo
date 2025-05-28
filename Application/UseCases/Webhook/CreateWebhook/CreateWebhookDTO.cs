namespace Application.UseCases.Webhook.CreateWebhook
{
    public record CreateWebhookInput(
        long TeamId,
        string Endpoint,
        List<string> Events,
        long? SpaceId,
        long? FolderId,
        string? TaskId,
        long? ListId
    );

    public record CreateWebhookOutput(
        Guid Id,
        WebHookInfoWithViewId Webhook
    );
}
