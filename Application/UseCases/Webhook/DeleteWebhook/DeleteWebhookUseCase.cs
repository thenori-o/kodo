using Application.Interfaces.Webhook;

namespace Application.UseCases.Webhook.DeleteWebhook
{
    public class DeleteWebhookUseCase
    {
        private readonly IWebhookService _webhookService;

        public DeleteWebhookUseCase(IWebhookService webhookService)
        {
            _webhookService = webhookService;
        }

        public async Task ExecuteAsync(DeleteWebhookInput input)
        {
            await _webhookService.DeleteAsync(input);
        }
    }
}
