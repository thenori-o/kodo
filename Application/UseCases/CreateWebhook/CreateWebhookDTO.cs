namespace Application.UseCases.CreateWebhook
{
    public record CreateWebhookInput(
        string Endpoint,
        List<string> Events,
        long? SpaceId,
        long? FolderId,
        string? TaskId,
        long? ListId
    );

    public record CreateWebhookOutput(
        Guid Id,
        WebHookInfo Webhook
    );
}
