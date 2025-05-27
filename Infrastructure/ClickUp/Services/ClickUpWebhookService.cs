using Application.Interfaces;
using Application.UseCases.CreateWebhook;
using Application.UseCases.DeleteWebhook;
using Application.UseCases.GetWebhooks;
using Application.UseCases.UpdateWebhook;
using Infrastructure.ClickUp.DTOs;
using Infrastructure.Config;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Infrastructure.ClickUp.Services
{
    public class ClickUpWebhookService : IWebhookService
    {
        private readonly KodoSettings _settings;
        private readonly HttpClient _httpClient;

        public ClickUpWebhookService(HttpClient httpClient, IOptions<KodoSettings> options)
        {
            _settings = options.Value;
            _httpClient = httpClient;

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<GetWebhooksOutput> GetAsync(GetWebhooksInput input)
        {
            var url = $"{_settings.ClickUp.ApiAddress}/team/{input.TeamId}/webhook";

            _httpClient.DefaultRequestHeaders.Add("Authorization", _settings.ClickUp.PersonalToken);
            using var response = await _httpClient.GetAsync(url).ConfigureAwait(false);
            var responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var deserializedJson = JsonSerializer.Deserialize<GetWebhookResponse>(responseJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (deserializedJson != null)
                    return new GetWebhooksOutput(deserializedJson.Webhooks.Select(w => new Application.UseCases.WebHookInfo(
                        w.Id,
                        w.UserId,
                        w.TeamId,
                        w.Endpoint,
                        w.ClientId,
                        new List<string>(w.Events),
                        w.TaskId,
                        w.ListId,
                        w.FolderId,
                        w.SpaceId,
                        new Application.UseCases.WebhookHealth(w.Health.Status, w.Health.FailCount),
                        w.Secret
                    )));

                throw new ApplicationException("A resposta da API foi bem-sucedida, mas não retornou dados esperados.");
            }
            else
            {
                var error = JsonSerializer.Deserialize<ErrorResponse>(responseJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var errorMessage = error != null
                    ? $"{error.Err} (Code: {error.ECode})"
                    : $"Erro desconhecido ao acessar a API. Status Code: {(int)response.StatusCode} - {response.ReasonPhrase}";

                throw new ApplicationException(errorMessage);
            }
        }

        public async Task<CreateWebhookOutput> CreateAsync(CreateWebhookInput input)
        {
            var url = $"{_settings.ClickUp.ApiAddress}/team/{input.TeamId}/webhook";

            var contentBody = new StringContent(
                JsonSerializer.Serialize(new CreateWebhookRequest
                {
                    Endpoint = input.Endpoint,
                    Events = input.Events,
                    SpaceId = input.SpaceId,
                    FolderId = input.FolderId,
                    TaskId = input.TaskId,
                    ListId = input.ListId,
                }),
                Encoding.UTF8,
                "application/json"
            );

            _httpClient.DefaultRequestHeaders.Add("Authorization", _settings.ClickUp.PersonalToken);
            using var response = await _httpClient.PostAsync(url, contentBody).ConfigureAwait(false);
            var responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var deserializedJson = JsonSerializer.Deserialize<CreateWebhookResponse>(responseJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (deserializedJson != null)
                    return new CreateWebhookOutput(
                        deserializedJson.Id,
                        new Application.UseCases.WebHookInfoWithViewId(
                            deserializedJson.Webhook.Id,
                            deserializedJson.Webhook.UserId,
                            deserializedJson.Webhook.TeamId,
                            deserializedJson.Webhook.Endpoint,
                            deserializedJson.Webhook.ClientId,
                            deserializedJson.Webhook.Events,
                            deserializedJson.Webhook.TaskId,
                            deserializedJson.Webhook.ListId,
                            deserializedJson.Webhook.FolderId,
                            deserializedJson.Webhook.SpaceId,
                            deserializedJson.Webhook.ViewId,
                            new Application.UseCases.WebhookHealth(
                                deserializedJson.Webhook.Health.Status,
                                deserializedJson.Webhook.Health.FailCount
                            ),
                            deserializedJson.Webhook.Secret
                        )
                    );

                throw new ApplicationException("A resposta da API foi bem-sucedida, mas não retornou dados esperados.");
            }
            else
            {
                var error = JsonSerializer.Deserialize<ErrorResponse>(responseJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var errorMessage = error != null
                    ? $"{error.Err} (Code: {error.ECode})"
                    : $"Erro desconhecido ao criar webhook. Status Code: {(int)response.StatusCode} - {response.ReasonPhrase}";

                throw new ApplicationException(errorMessage);
            }
        }

        public async Task<UpdateWebhookOutput> UpdateAsync(UpdateWebhookInput input)
        {
            var url = $"{_settings.ClickUp.ApiAddress}/webhook/{input.Id}";


            var contentBody = new StringContent(
                JsonSerializer.Serialize(new
                {
                    endpoint = input.Endpoint,
                    events = input.Events,
                    status = input.Status,
                }),
                Encoding.UTF8,
                "application/json"
            );

            _httpClient.DefaultRequestHeaders.Add("Authorization", _settings.ClickUp.PersonalToken);
            using var response = await _httpClient.PutAsync(url, contentBody).ConfigureAwait(false);
            var responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var deserializedJson = JsonSerializer.Deserialize<CreateWebhookResponse>(responseJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (deserializedJson != null)
                    return new UpdateWebhookOutput(deserializedJson.Id,
                        new Application.UseCases.WebHookInfo(
                            deserializedJson.Webhook.Id,
                            deserializedJson.Webhook.UserId,
                            deserializedJson.Webhook.TeamId,
                            deserializedJson.Webhook.Endpoint,
                            deserializedJson.Webhook.ClientId,
                            deserializedJson.Webhook.Events,
                            deserializedJson.Webhook.TaskId,
                            deserializedJson.Webhook.ListId,
                            deserializedJson.Webhook.FolderId,
                            deserializedJson.Webhook.SpaceId,
                            new Application.UseCases.WebhookHealth(
                                deserializedJson.Webhook.Health.Status,
                                deserializedJson.Webhook.Health.FailCount
                            ),
                            deserializedJson.Webhook.Secret
                        )
                    );

                throw new ApplicationException("A resposta da API foi bem-sucedida, mas não retornou dados esperados.");
            }
            else
            {
                var error = JsonSerializer.Deserialize<ErrorResponse>(responseJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var errorMessage = error != null
                    ? $"{error.Err} (Code: {error.ECode})"
                    : $"Erro desconhecido ao criar webhook. Status Code: {(int)response.StatusCode} - {response.ReasonPhrase}";

                throw new ApplicationException(errorMessage);
            }
        }

        public async Task DeleteAsync(DeleteWebhookInput input)
        {
            var url = $"{_settings.ClickUp.ApiAddress}/webhook/{input.WebhookId}";

            _httpClient.DefaultRequestHeaders.Add("Authorization", _settings.ClickUp.PersonalToken);
            using var response = await _httpClient.DeleteAsync(url).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
                return;
            else
            {
                var responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                var error = JsonSerializer.Deserialize<ErrorResponse>(responseJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var errorMessage = error != null
                    ? $"{error.Err} (Code: {error.ECode})"
                    : $"Erro desconhecido ao criar webhook. Status Code: {(int)response.StatusCode} - {response.ReasonPhrase}";

                throw new ApplicationException(errorMessage);
            }
        }
    }
}
