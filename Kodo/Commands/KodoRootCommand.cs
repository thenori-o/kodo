using System.CommandLine;
using System.Text;

namespace Kodo.Commands
{
    public class KodoRootCommand
    {
        private Command RootCommand;
        private readonly IEnumerable<ICommandHandler> _commands;

        public KodoRootCommand(IEnumerable<ICommandHandler> commands)
        {
            Console.OutputEncoding = Encoding.UTF8;

            _commands = commands;
            RootCommand = new RootCommand
            {
                Description = "Fandi KODO - Ferramenta unificada para configuração de integrações e automações"
            };

            Configure();
        }

        private void Configure()
        {
            foreach (var commandHandler in _commands)
                if (commandHandler != this)
                    RootCommand.AddCommand(commandHandler.GetCommand());

            RootCommand.SetHandler(() =>
            {
                Console.WriteLine(@"
                                                   ⠀⠀⢷⠀⢠⢣⡏⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
   ▄████████    ▄████████ ███▄▄▄▄   ████████▄   ▄█ ⠀⠀⢘⣷⢸⣾⣇⣶⣦⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
  ███    ███   ███    ███ ███▀▀▀██▄ ███   ▀███ ███ ⠀⠀⠀⣿⣿⣿⣹⣿⣿⣷⣿⣆⣀⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
  ███    █▀    ███    ███ ███   ███ ███    ███ ███▌⠀⠀⠀⢼⡇⣿⣿⣽⣶⣶⣯⣭⣷⣶⣿⣿⣶⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
 ▄███▄▄▄       ███    ███ ███   ███ ███    ███ ███▌⠀⠀⠀⠸⠣⢿⣿⣿⣿⣿⡿⣛⣭⣭⣭⡙⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
▀▀███▀▀▀     ▀███████████ ███   ███ ███    ███ ███▌⠀⠀⠀⠀⠸⣿⣿⣿⣿⣿⠿⠿⠿⢯⡛⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
  ███          ███    ███ ███   ███ ███    ███ ███ ⠀⠀⠀⠀⠀⠀⢠⣿⣿⣿⣿⣾⣿⡿⡷⢿⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
  ███          ███    ███ ███   ███ ███   ▄███ ███ ⠀⠀⠀⠀⠀⡔⣺⣿⣿⣽⡿⣿⣿⣿⣟⡳⠦⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
  ███          ███    █▀   ▀█   █▀  ████████▀  █▀  ⠀⠀⠀⠀⢠⣭⣾⣿⠃⣿⡇⣿⣿⡷⢾⣭⡓⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
   ▄█   ▄█▄  ▄██████▄  ████████▄   ▄██████▄        ⠀⠀⠀⣾⣿⡿⠷⣿⣿⡇⣿⣿⣟⣻⠶⣭⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
  ███ ▄███▀ ███    ███ ███   ▀███ ███    ███       ⠀⠀⠀⠀⣋⣵⣞⣭⣮⢿⣧⣝⣛⡛⠿⢿⣦⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
  ███▐██▀   ███    ███ ███    ███ ███    ███     ⠀⣀⣀⣠⣶⣿⣿⣿⣿⡿⠟⣼⣿⡿⣟⣿⡇⠀⠙
 ▄█████▀    ███    ███ ███    ███ ███    ███     ⡼⣿⣿⣿⢟⣿⣿⣿⣷⡿⠿⣿⣿⣿⣿⣿
▀▀█████▄    ███    ███ ███    ███ ███    ███     ⠀⠀⠉⠁⠀⢉⣭⣭⣽⣯⣿⣿⢿⣫⣮⣅⣀
  ███▐██▄   ███    ███ ███    ███ ███    ███     ⠀⠀⠀⠀⢀⣿⣟⣽⣿⣿⣿⣿⣾⣿⣿⣯⡛⠻⢷⣶⣤⣄⡀
  ███   ▀█▀  ▀██████▀  ████████▀   ▀██████▀      ⠀⠀⠀⢀⡞⣾⣿⣿⣿⣿⡟⣿⣿⣽⣿⣿⡿⠀⠀⠀⠈⠙⠛⠿⣶⣤⣄⡀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀                             ⠀⠀⠀⣾⣸⣿⣿⣷⣿⣿⢧⣿⣿⣿⣿⣿⣷⠁⠀⠀⠀⠀⠀⠀⠀⠈⠙⠻⢷⣦
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀                         ⠀⠀
Comandos disponíveis:
  webhook    Gerenciar webhooks do ClickUp
  version    Mostrar a versão atual
  help       Mostrar ajuda detalhada

Exemplos de uso:
  kodo webhook create --teamId 123 --endpoint https://meu.site/webhook --events taskCreated --listId 456
  kodo webhook get --teamId 123

Use 'kodo [comando] --help' para mais informações sobre cada comando.
");
            });
        }

        public Command GetCommand() => RootCommand;
    }
}
