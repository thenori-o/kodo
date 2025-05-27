using System.CommandLine;

namespace Kodo.Commands
{
    public static class WebhookCommand
    {

        public static Command Build()
        {
            var webhook = new Command("webhook", "Gerencia webhooks do ClickUp");

            webhook.AddCommand(CreateCommand());
            webhook.AddCommand(ListCommand());
            webhook.AddCommand(UpdateCommand());
            webhook.AddCommand(DeleteCommand());

            return webhook;
        }

        private static Command CreateCommand()
        {
            var cmd = new Command("create", "Cria um novo webhook");
            var endpointOpt = new Option<string>("--endpoint", "URL do seu webhook") { IsRequired = true };
            var eventsOpt = new Option<string[]>("--events", "Eventos do ClickUp") { IsRequired = true };

            cmd.AddOption(endpointOpt);
            cmd.AddOption(eventsOpt);

            cmd.SetHandler(async (string endpoint, string[] events) =>
            {
                var service = new ClickUpWebhookService(token, teamId);

                await service.CreateWebhookAsync(new WebhookRequest
                {
                    Endpoint = endpoint,
                    Events = events.ToList()
                });
            }, endpointOpt, eventsOpt);

            return cmd;
        }

        private static Command ListCommand()
        {
            var cmd = new Command("list", "Lista os webhooks existentes");

            cmd.SetHandler(async () =>
            {
                var (token, teamId) = LoadConfig();
                var service = new ClickUpWebhookService(token, teamId);
                var hooks = await service.ListWebhooksAsync();

                foreach (var hook in hooks)
                    Console.WriteLine($"{hook.Id} => {hook.Endpoint}");
            });

            return cmd;
        }

        private static Command UpdateCommand()
        {
            var cmd = new Command("update", "Atualiza um webhook existente");

            var idOpt = new Option<string>("--id", "ID do webhook") { IsRequired = true };
            var endpointOpt = new Option<string>("--endpoint", "Novo endpoint");
            var eventsOpt = new Option<string[]>("--events", "Novos eventos");

            cmd.AddOption(idOpt);
            cmd.AddOption(endpointOpt);
            cmd.AddOption(eventsOpt);

            cmd.SetHandler(async (string id, string? endpoint, string[]? events) =>
            {
                var (token, teamId) = LoadConfig();
                var service = new ClickUpWebhookService(token, teamId);

                await service.UpdateWebhookAsync(id, endpoint, events?.ToList());
            }, idOpt, endpointOpt, eventsOpt);

            return cmd;
        }

        private static Command DeleteCommand()
        {
            var cmd = new Command("delete", "Remove um webhook");

            var idOpt = new Option<string>("--id", "ID do webhook") { IsRequired = true };
            cmd.AddOption(idOpt);

            cmd.SetHandler(async (string id) =>
            {
                var (token, teamId) = LoadConfig();
                var service = new ClickUpWebhookService(token, teamId);

                await service.DeleteWebhookAsync(id);
            }, idOpt);

            return cmd;
        }

        private static (string Token, string TeamId) LoadConfig()
        {
            var config = System.Text.Json.JsonDocument.Parse(File.ReadAllText("appsettings.json"))
                             .RootElement.GetProperty("ClickUp");
            return (
                config.GetProperty("Token").GetString()!,
                config.GetProperty("TeamId").GetString()!
            );
        }
    }
}
