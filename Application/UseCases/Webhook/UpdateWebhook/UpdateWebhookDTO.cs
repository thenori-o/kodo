﻿namespace Application.UseCases.Webhook.UpdateWebhook
{
    public record UpdateWebhookInput(Guid Id, string Endpoint, List<string> Events, string Status);

    public record UpdateWebhookOutput(
        Guid Id,
        WebHookInfo Webhook
    );
}
