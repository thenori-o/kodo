using ClickUpSdk.DTOs;
using ClickUpSdk.DTOs.Webhook;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ClickUpSdk.Client
{
    public class ClickUpClient
    {
        private readonly ClickUpSettings _settings;
        private readonly HttpClient _httpClient;

        public ClickUpClient(HttpClient httpClient, IOptions<ClickUpSettings> options)
        {
            _settings = options.Value;
            _httpClient = httpClient;

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<GetWebhookResponse> GetWebhooksAsync(long teamId)
        {
            var url = $"{_settings.ApiAddress}/team/{teamId}/webhook";

            _httpClient.DefaultRequestHeaders.Add("Authorization", _settings.PersonalToken);
            using var response = await _httpClient.GetAsync(url).ConfigureAwait(false);
            var responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<GetWebhookResponse>(responseJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (result != null)
                    return result;

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

        public async Task<CreateWebhookResponse> CreateWebhookAsync(CreateWebhookRequest request)
        {
            var url = $"{_settings.ApiAddress}/team/{request.TeamId}/webhook";

            var requestBody = new CreateWebhookRequest
            {
                Endpoint = request.Endpoint,
                Events = request.Events,
                SpaceId = request.SpaceId,
                FolderId = request.FolderId,
                TaskId = request.TaskId,
                ListId = request.ListId,
            };

            var contentBody = new StringContent(
                JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json"
            );

            _httpClient.DefaultRequestHeaders.Add("Authorization", _settings.PersonalToken);
            using var response = await _httpClient.PostAsync(url, contentBody).ConfigureAwait(false);
            var responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<CreateWebhookResponse>(responseJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (result != null)
                    return result;

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

        public async Task<CreateWebhookResponse> UpdateWebhookAsync(Guid id, UpdateWebhookRequest request)
        {
            var url = $"{_settings.ApiAddress}/webhook/{id}";


            var contentBody = new StringContent(
                JsonSerializer.Serialize(new
                {
                    endpoint = request.Endpoint,
                    events = request.Events,
                    status = request.Status,
                }),
                Encoding.UTF8,
                "application/json"
            );

            _httpClient.DefaultRequestHeaders.Add("Authorization", _settings.PersonalToken);
            using var response = await _httpClient.PutAsync(url, contentBody).ConfigureAwait(false);
            var responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<CreateWebhookResponse>(responseJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (result != null)
                    return result;

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

        public async Task DeleteWebhookAsync(Guid webhookId)
        {
            var url = $"{_settings.ApiAddress}/webhook/{webhookId}";

            _httpClient.DefaultRequestHeaders.Add("Authorization", _settings.PersonalToken);
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

        //private async Task<string> GetClickUpAccessTokenAsync()
        //{
        //    var url = $"{_settings.ApiAddress}/oauth/token" +
        //              $"?client_id={_settings.ClientId}" +
        //              $"&client_secret={_settings.ClientSecret}" +
        //              $"&code={_settings.Code}";

        //    var response = await _httpClient.PostAsync(url, null);
        //    var content = await response.Content.ReadAsStringAsync();

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var success = JsonSerializer.Deserialize<AccessTokenResponse>(content);
        //        return success?.AccessToken ?? throw new ApplicationException("Token de acesso não encontrado na resposta.");
        //    }

        //    var error = JsonSerializer.Deserialize<ErrorResponse>(content);
        //    throw new ApplicationException($"Erro ao obter token: {error?.Err} (Código: {error?.ECode})");
        //}
    }
}
