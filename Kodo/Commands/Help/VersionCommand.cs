using System.CommandLine;

namespace Kodo.Commands.Help
{
    public class VersionCommand : ICommandHandler
    {
        public Command GetCommand()
        {
            var cmd = new Command("version", "Exibe a versão do Kodo");

            cmd.SetHandler((id) =>
            {
                Console.WriteLine("v1.0.0");
            });

            return cmd;
        }
    }
}
