using Infrastructure.Config;
using Microsoft.Extensions.Options;
using System.CommandLine;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Kodo.Commands.Config
{
    public class ConfigCommand : ICommandHandler
    {

        private readonly IOptions<KodoSettings> _settings;

        public ConfigCommand(IOptions<KodoSettings> settings)
        {
            _settings = settings;
        }

        public Command GetCommand()
        {
            var command = new Command("config", "Configurações do Kodo");

            var hostOption = new Option<string>("--host", "Definir host do webhook da sua aplicação");
            var apiAddressOption = new Option<string>("--apiAddress", "Definir endereço da api do ClickUp");
            var clientIdOption = new Option<string>("--clientId", "Definir clientId do App associado ao seu workspace");
            var clientSecretOption = new Option<string>("--clientSecret", "Definir clientSecret do App associado ao seu workspace");
            var personalTokenOption = new Option<string>("--personalToken", "Definir personalToken do seu workspace");

            command.AddOption(hostOption);
            command.AddOption(apiAddressOption);
            command.AddOption(clientIdOption);
            command.AddOption(clientSecretOption);
            command.AddOption(personalTokenOption);

            command.SetHandler((host, apiAddress, clientId, clientSecret, personalToken) =>
            {
                var hasUpdates = !string.IsNullOrWhiteSpace(host)
                  || !string.IsNullOrWhiteSpace(apiAddress)
                  || !string.IsNullOrWhiteSpace(clientId)
                  || !string.IsNullOrWhiteSpace(clientSecret)
                  || !string.IsNullOrWhiteSpace(personalToken);

                if (!hasUpdates)
                {
                    ListAllSettings();
                    return;
                }

                try
                {
                    KodoSettingsManager.UpdateKodoSettings(settings =>
                    {
                        if (!string.IsNullOrWhiteSpace(host))
                            settings["Host"] = host;

                        if (settings["ClickUp"] is not JsonObject clickUp)
                        {
                            clickUp = new JsonObject();
                            settings["ClickUp"] = clickUp;
                        }

                        if (!string.IsNullOrWhiteSpace(apiAddress))
                            clickUp["ApiAddress"] = apiAddress;

                        if (!string.IsNullOrWhiteSpace(clientId))
                            clickUp["ClientId"] = clientId;

                        if (!string.IsNullOrWhiteSpace(clientSecret))
                            clickUp["ClientSecret"] = clientSecret;

                        if (!string.IsNullOrWhiteSpace(personalToken))
                            clickUp["PersonalToken"] = personalToken;
                    });

                    Console.WriteLine("Configurações atualizadas com sucesso.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao atualizar configurações: " + ex.Message);
                }
            },
            hostOption, apiAddressOption, clientIdOption, clientSecretOption, personalTokenOption);
            return command;
        }

        private void ListAllSettings()
        {
            var settings = _settings.Value;
            Console.WriteLine("KodoSettings:");
            Console.WriteLine($"  Host: {settings.Host}");
            Console.WriteLine($"  ApiAddress: {settings.ClickUp.ApiAddress}");
            Console.WriteLine($"  ClientId: {settings.ClickUp.ClientId}");
            Console.WriteLine($"  ClientSecret: {settings.ClickUp.ClientSecret}");
            Console.WriteLine($"  PersonalToken: {settings.ClickUp.PersonalToken}");
        }

        private void UpdateSetting(string key, string value)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");

            var json = File.ReadAllText(path);
            var jsonObj = JsonNode.Parse(json)?.AsObject();

            if (jsonObj is null || !jsonObj.ContainsKey("KodoSettings"))
            {
                Console.WriteLine("KodoSettings não encontrado no appsettings.json");
                return;
            }

            var settingsObj = jsonObj["KodoSettings"]?.AsObject();
            if (settingsObj is null)
            {
                Console.WriteLine("Seção KodoSettings está inválida.");
                return;
            }

            if (!settingsObj.ContainsKey(key))
            {
                Console.WriteLine($"Chave '{key}' não encontrada em KodoSettings.");
                return;
            }

            settingsObj[key] = value;
            File.WriteAllText(path, jsonObj.ToJsonString(new JsonSerializerOptions { WriteIndented = true }));

            Console.WriteLine($"Chave '{key}' atualizada para '{value}'.");
        }
    }
}
