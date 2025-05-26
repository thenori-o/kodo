using Application.Interfaces;

namespace Application.UseCases.CreateWebhook
{
    public class CreateWebhookUseCase
    {
        private readonly IWebhookService _webhookService;

        public CreateWebhookUseCase(IWebhookService webhookService)
        {
            _webhookService = webhookService;
        }

        public async Task<CreateWebhookOutput> ExecuteAsync(CreateWebhookInput input)
        {
            return await _webhookService.CreateAsync(input);
        }
    }
}
