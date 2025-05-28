using Application.UseCases.Webhook.GetWebhooks;
using Kodo.Utils;
using System.CommandLine;

namespace Kodo.Commands.Webhook
{
    public class ListWebhooksSubCommand : ISubCommandHandler
    {
        private readonly GetWebhooksUseCase _getWebhooksUseCase;

        public ListWebhooksSubCommand(GetWebhooksUseCase getWebhooksUseCase)
        {
            _getWebhooksUseCase = getWebhooksUseCase;
        }

        public Command GetCommand()
        {
            var cmd = new Command("list", "Lista os webhooks existentes");
            var teamIdOpt = new Option<long>("--team_id", "Id do seu workspace") { IsRequired = true };

            cmd.AddOption(teamIdOpt);

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
    }
}
