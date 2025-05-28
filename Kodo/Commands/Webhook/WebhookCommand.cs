using System.CommandLine;

namespace Kodo.Commands.Webhook
{
    public class WebhookCommand : ICommandHandler
    {
        private readonly IEnumerable<ISubCommandHandler> _subcommands;

        public WebhookCommand(IEnumerable<ISubCommandHandler> subcommands)
        {
            _subcommands = subcommands;
        }

        public Command GetCommand()
        {
            Command Command = new("webhook", "Gerencia webhooks  do ClickUp");

            foreach (var subcommand in _subcommands)
                if (subcommand != this)
                    Command.AddCommand(subcommand.GetCommand());

            return Command;
        }
    }
}
