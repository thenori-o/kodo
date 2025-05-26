using Application.Interfaces;

namespace Application.UseCases.GetWebhooks
{
    public class GetWebhooksUseCase
    {
        private readonly IWebhookService _webhookService;

        public GetWebhooksUseCase(IWebhookService webhookService)
        {
            _webhookService = webhookService;
        }

        public async Task<GetWebhooksOutput> ExecuteAsync(GetWebhooksInput input)
        {
            return await _webhookService.GetAsync(input);
        }
    }
}
