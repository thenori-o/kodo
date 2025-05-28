using Application.UseCases.Webhook.CreateWebhook;
using Application.UseCases.Webhook.DeleteWebhook;
using Application.UseCases.Webhook.GetWebhooks;
using Application.UseCases.Webhook.UpdateWebhook;

namespace Application.Interfaces.Webhook
{
    public interface IWebhookService
    {
        Task<CreateWebhookOutput> CreateAsync(CreateWebhookInput input);
        Task<GetWebhooksOutput> GetAsync(GetWebhooksInput input);
        Task<UpdateWebhookOutput> UpdateAsync(UpdateWebhookInput input);
        Task DeleteAsync(DeleteWebhookInput input);
    }
}
