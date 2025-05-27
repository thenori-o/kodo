using Kodo.Commands;
using Microsoft.Extensions.DependencyInjection;
using System.CommandLine;

var services = new ServiceCollection();


var root = KodoCommand.Build();

await root.InvokeAsync(args);