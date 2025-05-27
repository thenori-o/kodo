using Infrastructure.Config;
using Kodo.Commands;
using Microsoft.Extensions.Options;
using System.CommandLine;

namespace Kodo
{
    public class App
    {
        private readonly KodoSettings _settings;
        private readonly KodoRootCommand _kodoCommand;

        public App(IOptions<KodoSettings> settings, KodoRootCommand kodoCommand)
        {
            _settings = settings.Value;
            _kodoCommand = kodoCommand;
        }

        public async Task<int> RunAsync(string[] args)
        {
            return await _kodoCommand.GetCommand().InvokeAsync(args);
        }
    }
}
