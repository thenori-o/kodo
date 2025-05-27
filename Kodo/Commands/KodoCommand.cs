using System.CommandLine;

namespace Kodo.Commands
{
    public static class KodoCommand
    {

        public static Command Build()
        {
            var rootCommand = new RootCommand("Kodo CLI - Ferramenta para interagir com ClickUp");

            rootCommand.SetHandler(() =>
            {
                Console.WriteLine(@"
                                                        ⢠  ⠀⠀⠀⢠⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀ 
                                                     ⠀⠀⠀⠀⢷⠀⢠⢣⡏⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
                                                     ⠀⠀⠀⠀⢘⣷⢸⣾⣇⣶⣦⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
                                                     ⠀⠀⠀⠀⠀⣿⣿⣿⣹⣿⣿⣷⣿⣆⣀⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
   ▄████████    ▄████████ ███▄▄▄▄   ████████▄   ▄█   ⠀⠀⠀⠀⠀⢼⡇⣿⣿⣽⣶⣶⣯⣭⣷⣶⣿⣿⣶⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
  ███    ███   ███    ███ ███▀▀▀██▄ ███   ▀███ ███   ⠀⠀⠀⠀⠀⠸⠣⢿⣿⣿⣿⣿⡿⣛⣭⣭⣭⡙⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
  ███    █▀    ███    ███ ███   ███ ███    ███ ███▌  ⠀⠀⠀⠀⠀⠀⠀⠸⣿⣿⣿⣿⣿⠿⠿⠿⢯⡛⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
 ▄███▄▄▄       ███    ███ ███   ███ ███    ███ ███▌  ⠀⠀⠀⠀⠀⠀⠀⠀⢠⣿⣿⣿⣿⣾⣿⡿⡷⢿⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
▀▀███▀▀▀     ▀███████████ ███   ███ ███    ███ ███▌  ⠀⠀⠀⠀⠀⠀⠀⡔⣺⣿⣿⣽⡿⣿⣿⣿⣟⡳⠦⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
  ███          ███    ███ ███   ███ ███    ███ ███   ⠀⠀⠀⠀⠀⠀⢠⣭⣾⣿⠃⣿⡇⣿⣿⡷⢾⣭⡓⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
  ███          ███    ███ ███   ███ ███   ▄███ ███   ⠀⠀⠀⠀⠀⠀⣾⣿⡿⠷⣿⣿⡇⣿⣿⣟⣻⠶⣭⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
  ███          ███    █▀   ▀█   █▀  ████████▀  █▀    ⠀⠀⠀⠀⠀⠀⣋⣵⣞⣭⣮⢿⣧⣝⣛⡛⠿⢿⣦⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
   ▄█   ▄█▄  ▄██████▄  ████████▄   ▄██████▄          ⠀⣀⣀⣠⣶⣿⣿⣿⣿⡿⠟⣼⣿⡿⣟⣿⡇⠀⠙
  ███ ▄███▀ ███    ███ ███   ▀███ ███    ███         ⡼⣿⣿⣿⢟⣿⣿⣿⣷⡿⠿⣿⣿⣿⣿⣿
  ███▐██▀   ███    ███ ███    ███ ███    ███         ⠀⠀⠉⠁⠀⢉⣭⣭⣽⣯⣿⣿⢿⣫⣮⣅⣀
 ▄█████▀    ███    ███ ███    ███ ███    ███         ⠀⠀⠀⠀⢀⣿⣟⣽⣿⣿⣿⣿⣾⣿⣿⣯⡛⠻⢷⣶⣤⣄⡀
▀▀█████▄    ███    ███ ███    ███ ███    ███         ⠀⠀⠀⢀⡞⣾⣿⣿⣿⣿⡟⣿⣿⣽⣿⣿⡿⠀⠀⠀⠈⠙⠛⠿⣶⣤⣄⡀
  ███▐██▄   ███    ███ ███    ███ ███    ███         ⠀⠀⠀⣾⣸⣿⣿⣷⣿⣿⢧⣿⣿⣿⣿⣿⣷⠁⠀⠀⠀⠀⠀⠀⠀⠈⠙⠻⢷⣦
  ███   ▀█▀  ▀██████▀  ████████▀   ▀██████▀          ⠀⠀⠀⡿⣛⣛⣛⣛⣿⣿⣸⣿⣿⣿⣻⣿⣿⠆
  ▀                                                  ⠀⠀⢸⡇⣿⣿⣿⣿⣿⡏⣿⣿⣿⣿⣿⣿⣿⡇
                                                     ⠀⠀⠀⢰⣶⣶⣶⣾⣿⢃⣿⣿⣿⣿⣯⣿⣭⠁
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀                         ⠀⠀
Comandos disponíveis:
  webhook    Gerenciar webhooks do ClickUp
  version    Mostrar a versão atual
  help       Mostrar ajuda detalhada

Exemplos de uso:
  kodo webhook create --teamId 123 --endpoint https://meu.webhook --events taskCreated --listId 456
  kodo webhook get --teamId 123

Use 'kodo [comando] --help' para mais informações sobre cada comando.
");
            });

            return rootCommand;
        }

    }
}
