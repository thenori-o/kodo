namespace Application.UseCases.Webhook
{
    public record class WebHookInfo(
        Guid Id,
        long UserId,
        long TeamId,
        string Endpoint,
        string ClientId,
        List<string> Events,
        string? TaskId,
        long? ListId,
        long? FolderId,
        long? SpaceId,
        WebhookHealth Health,
        string Secret
    );

    public record class WebHookInfoWithViewId(Guid Id,
        long UserId,
        long TeamId,
        string Endpoint,
        string ClientI,
        List<string> Events,
        string? TaskId,
        long? ListId,
        long? FolderId,
        long? SpaceId,
        string? ViewId,
        WebhookHealth Health,
        string Secret) : WebHookInfo(
            Id,
            UserId,
            TeamId,
            Endpoint,
            ClientI,
            Events,
            TaskId,
            ListId,
            FolderId,
            SpaceId,
            Health,
            Secret
        );

    public record WebhookHealth(string Status, int FailCount);
}
