using Application.UseCases.Webhook.CreateWebhook;
using Kodo.Utils;
using System.CommandLine;

namespace Kodo.Commands.Webhook
{
    public class CreateWebhookSubCommand : ISubCommandHandler
    {
        private readonly CreateWebhookUseCase _createWebhookUseCase;

        public CreateWebhookSubCommand(CreateWebhookUseCase createWebhookUseCase)
        {
            _createWebhookUseCase = createWebhookUseCase;
        }

        public Command GetCommand()
        {
            var cmd = new Command("create", "Cria um novo webhook");
            var teamIdOpt = new Option<long>("--team_id", "Id do seu workspace") { IsRequired = true };
            var endpointOpt = new Option<string>("--endpoint", "URL do seu webhook") { IsRequired = true };
            var eventsOpt = new Option<List<string>>("--events", "Eventos do ClickUp") { IsRequired = true };
            var spaceIdOpt = new Option<long>("--space_id", "Id do seu espaço") { IsRequired = true };
            var folderIdOpt = new Option<long>("--folder_id", "Id da sua pasta") { IsRequired = true };
            var listIdOpt = new Option<long>("--list_id", "Id da sua lista") { IsRequired = true };
            var taskIdOpt = new Option<string>("--task_id", "Id da sua tarefa") { IsRequired = true };

            cmd.AddOption(teamIdOpt);
            cmd.AddOption(endpointOpt);
            cmd.AddOption(eventsOpt);
            cmd.AddOption(spaceIdOpt);
            cmd.AddOption(folderIdOpt);
            cmd.AddOption(listIdOpt);
            cmd.AddOption(taskIdOpt);

            cmd.SetHandler(
                async (teamId, endpoint, events, spaceId, folderId, taskId, listId) =>
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
                teamIdOpt, endpointOpt, eventsOpt, spaceIdOpt, folderIdOpt, taskIdOpt, listIdOpt
            );

            return cmd;
        }
    }
}
