using Application.UseCases.CreateWebhook;
using Application.UseCases.DeleteWebhook;
using Application.UseCases.GetWebhooks;
using Application.UseCases.UpdateWebhook;

namespace Application.Interfaces
{
    public interface IWebhookService
    {
        Task<CreateWebhookOutput> CreateAsync(CreateWebhookInput input);
        Task<GetWebhooksOutput> GetAsync(GetWebhooksInput input);
        Task<UpdateWebhookOutput> UpdateAsync(UpdateWebhookInput input);
        Task DeleteAsync(DeleteWebhookInput input);
    }
}
