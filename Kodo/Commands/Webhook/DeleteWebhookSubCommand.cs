using System.CommandLine;

namespace Kodo.Commands.Webhook
{
    public class DeleteWebhookSubCommand : ISubCommandHandler
    {
        public Command GetCommand()
        {
            var cmd = new Command("delete", "Remove um webhook");

            var idOpt = new Option<string>("--id", "ID do webhook") { IsRequired = true };
            cmd.AddOption(idOpt);

            cmd.SetHandler((id) =>
            {
                throw new NotImplementedException();
            }, idOpt);

            return cmd;
        }
    }
}
