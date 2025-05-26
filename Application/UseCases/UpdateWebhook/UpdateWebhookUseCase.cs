using Application.Interfaces;

namespace Application.UseCases.UpdateWebhook
{
    public class UpdateWebhookUseCase
    {
        private readonly IWebhookService _webhookService;

        public UpdateWebhookUseCase(IWebhookService webhookService)
        {
            _webhookService = webhookService;
        }

        public async Task<UpdateWebhookOutput> ExecuteAsync(UpdateWebhookInput input)
        {
            return await _webhookService.UpdateAsync(input);
        }
    }
}
