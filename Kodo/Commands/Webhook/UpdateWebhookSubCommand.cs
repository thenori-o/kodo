using System.CommandLine;

namespace Kodo.Commands.Webhook
{
    public class UpdateWebhookSubCommand : ISubCommandHandler
    {
        public Command GetCommand()
        {
            var cmd = new Command("update", "Atualiza um webhook existente");

            var idOpt = new Option<string>("--id", "ID do webhook") { IsRequired = true };
            var endpointOpt = new Option<string>("--endpoint", "Novo endpoint");
            var eventsOpt = new Option<string[]>("--events", "Novos eventos");

            cmd.AddOption(idOpt);
            cmd.AddOption(endpointOpt);
            cmd.AddOption(eventsOpt);

            cmd.SetHandler((id, endpoint, events) =>
            {
                throw new NotImplementedException();
            }, idOpt, endpointOpt, eventsOpt);

            return cmd;
        }
    }
}
