using Application.UseCases.CreateWebhook;
using Application.UseCases.GetWebhooks;
using Infrastructure.Config;
using Kodo.Utils;
using Microsoft.Extensions.Options;
using System.CommandLine;

namespace Kodo.Commands
{
    public class WebhookCommand : ICommandHandler
    {
        private Command Command;
        private readonly KodoSettings _settings;

        private readonly CreateWebhookUseCase _createWebhookUseCase;
        private readonly GetWebhooksUseCase _getWebhooksUseCase;

        public WebhookCommand(IOptions<KodoSettings> options, CreateWebhookUseCase createWebhookUseCase, GetWebhooksUseCase getWebhooksUseCase)
        {
            _settings = options.Value;

            _createWebhookUseCase = createWebhookUseCase;
            _getWebhooksUseCase = getWebhooksUseCase;

            Command = new Command("webhook", "Gerencia webhooks  do ClickUp");
        }

        public Command GetCommand()
        {
            Command.AddOption(new Option<string>("--teamId", "ID do time") { IsRequired = true });
            Command.AddOption(new Option<string>("--endpoint", "URL do webhook") { IsRequired = true });

            Command.AddCommand(CreateCommand());
            Command.AddCommand(ListCommand());
            Command.AddCommand(UpdateCommand());
            Command.AddCommand(DeleteCommand());

            return Command;
        }

        private Command CreateCommand()
        {
            var cmd = new Command("create", "Cria um novo webhook");
            var teamIdOpt = new Option<long>("--team_id", "Id do seu workspace") { IsRequired = true };
            var endpointOpt = new Option<string>("--endpoint", "URL do seu webhook") { IsRequired = true };
            var eventsOpt = new Option<List<string>>("--events", "Eventos do ClickUp") { IsRequired = true };
            var spaceIdOpt = new Option<long>("--space_id", "Id do seu espaço") { IsRequired = true };
            var folderIdOpt = new Option<long>("--folder_id", "Id da sua pasta") { IsRequired = true };
            var listIdOpt = new Option<long>("--list_id", "Id da sua lista") { IsRequired = true };
            var taskIdOpt = new Option<string>("--task_id", "Id da sua tarefa") { IsRequired = true };

            cmd.AddOption(endpointOpt);
            cmd.AddOption(eventsOpt);

            cmd.SetHandler(
                async (teamId, endpoint, events, spaceId, folderId, listId, taskId) =>
                {
                    CreateWebhookOutput? output = null;
                    try
                    {
                        using (var spinner = new Spinner("Criando webhook"))
                        {
                            output = await _createWebhookUseCase.ExecuteAsync(
                            new CreateWebhookInput(teamId, endpoint, events, spaceId, folderId, taskId, listId));
                        }
                        if (output != null)
                        {
                            Console.WriteLine("✅ Webhook criado com sucesso!");
                            Console.WriteLine("--------------------------------------------------------");

                            Console.WriteLine($"🔗 Webhook ID: {output.Id}");
                            Console.WriteLine($"👤 Usuário ID: {output.Webhook.UserId}");
                            Console.WriteLine($"🏢 Time ID: {output.Webhook.TeamId}");
                            Console.WriteLine($"📬 Endpoint: {output.Webhook.Endpoint}");
                            Console.WriteLine($"🧾 Client ID: {output.Webhook.ClientI}");

                            Console.WriteLine("\n📡 Eventos:");
                            foreach (var evt in output.Webhook.Events)
                                Console.WriteLine($"  - {evt}");

                            Console.WriteLine("\n📌 Identificadores adicionais:");
                            Console.WriteLine($"  • Task ID: {output.Webhook.TaskId ?? "(não informado)"}");
                            Console.WriteLine($"  • List ID: {output.Webhook.ListId?.ToString() ?? "(não informado)"}");
                            Console.WriteLine($"  • Folder ID: {output.Webhook.FolderId?.ToString() ?? "(não informado)"}");
                            Console.WriteLine($"  • Space ID: {output.Webhook.SpaceId?.ToString() ?? "(não informado)"}");
                            Console.WriteLine($"  • View ID: {output.Webhook.ViewId ?? "(não informado)"}");

                            Console.WriteLine("\n📈 Saúde do Webhook:");
                            Console.WriteLine($"  • Status: {output.Webhook.Health.Status}");
                            Console.WriteLine($"  • Falhas consecutivas: {output.Webhook.Health.FailCount}");

                            Console.WriteLine("\n🔐 Segredo:");
                            Console.WriteLine($"  {output.Webhook.Secret}");

                            Console.WriteLine("\n\n");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"⚠ Erro ao criar webhook: {ex.Message}");
                    }
                },
                teamIdOpt, endpointOpt, eventsOpt, spaceIdOpt, folderIdOpt, listIdOpt, taskIdOpt
            );

            return cmd;
        }

        private Command ListCommand()
        {
            var cmd = new Command("list", "Lista os webhooks existentes");
            var teamIdOpt = new Option<long>("--team_id", "Id do seu workspace") { IsRequired = true };

            cmd.SetHandler(
                async (teamId) =>
                {
                    GetWebhooksOutput? output = null;
                    try
                    {
                        using (var spinner = new Spinner("Obtendo webhooks"))
                        {
                            output = await _getWebhooksUseCase.ExecuteAsync(new GetWebhooksInput(teamId));
                        }
                        if (output != null)
                        {
                            var webhooks = output.Webhooks.ToList();

                            if (webhooks?.Count == 0)
                            {
                                Console.WriteLine("📭 Nenhum webhook encontrado.");
                                return;
                            }

                            Console.WriteLine($"📋 Total de webhooks encontrados: {webhooks?.Count}");
                            Console.WriteLine("========================================================");

                            for (int i = 0; i < webhooks?.Count; i++)
                            {
                                var webhook = webhooks[i];
                                Console.WriteLine($"🔹 Webhook #{i + 1}");
                                Console.WriteLine("--------------------------------------------------------");

                                Console.WriteLine($"🔗 ID: {webhook.Id}");
                                Console.WriteLine($"👤 Usuário ID: {webhook.UserId}");
                                Console.WriteLine($"🏢 Time ID: {webhook.TeamId}");
                                Console.WriteLine($"📬 Endpoint: {webhook.Endpoint}");
                                Console.WriteLine($"🧾 Client ID: {webhook.ClientId}");

                                Console.WriteLine($"\n📡 Eventos: ");
                                Console.WriteLine($"{string.Join(",", webhook.Events)}");

                                Console.WriteLine("\n📌 Identificadores adicionais:");
                                Console.WriteLine($"  • Task ID: {webhook.TaskId ?? "(não informado)"}");
                                Console.WriteLine($"  • List ID: {webhook.ListId?.ToString() ?? "(não informado)"}");
                                Console.WriteLine($"  • Folder ID: {webhook.FolderId?.ToString() ?? "(não informado)"}");
                                Console.WriteLine($"  • Space ID: {webhook.SpaceId?.ToString() ?? "(não informado)"}");

                                Console.WriteLine("\n📈 Saúde do Webhook:");
                                Console.WriteLine($"  • Status: {webhook.Health.Status}");
                                Console.WriteLine($"  • Falhas consecutivas: {webhook.Health.FailCount}");

                                Console.WriteLine("\n🔐 Segredo:");
                                Console.WriteLine($"  {webhook.Secret}");

                                Console.WriteLine("\n\n");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"⚠ Erro ao criar webhook: {ex.Message}");
                    }

                },
                teamIdOpt
            );

            return cmd;
        }

        private Command UpdateCommand()
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
                //var service = new ClickUpWebhookService(token, teamId);

                //await service.UpdateWebhookAsync(id, endpoint, events?.ToList());
            }, idOpt, endpointOpt, eventsOpt);

            return cmd;
        }

        private Command DeleteCommand()
        {
            var cmd = new Command("delete", "Remove um webhook");

            var idOpt = new Option<string>("--id", "ID do webhook") { IsRequired = true };
            cmd.AddOption(idOpt);

            cmd.SetHandler(async (string id) =>
            {
                var (token, teamId) = LoadConfig();
                //var service = new ClickUpWebhookService(token, teamId);

                //await service.DeleteWebhookAsync(id);
            }, idOpt);

            return cmd;
        }

        private (string Token, string TeamId) LoadConfig()
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
