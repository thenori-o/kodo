using Application.Interfaces.Webhook;
using Application.UseCases.Webhook.CreateWebhook;
using Application.UseCases.Webhook.DeleteWebhook;
using Application.UseCases.Webhook.GetWebhooks;
using Application.UseCases.Webhook.UpdateWebhook;
using Infrastructure.ClickUp.Services;
using Infrastructure.Config;
using Kodo;
using Kodo.Commands;
using Kodo.Commands.Config;
using Kodo.Commands.Help;
using Kodo.Commands.Webhook;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var kodoHome = Environment.GetEnvironmentVariable("KODO_HOME");

if (string.IsNullOrWhiteSpace(kodoHome) || !Directory.Exists(kodoHome))
{
    Console.WriteLine("⚠ Erro: a variável de ambiente 'KODO_HOME' não está definida ou o diretório não existe.");
    Environment.Exit(1);
}

var configuration = new ConfigurationBuilder()
    .SetBasePath(kodoHome)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var services = new ServiceCollection();

services.Configure<KodoSettings>(options =>
    configuration.GetSection("KodoSettings").Bind(options));

services.AddHttpClient<ClickUpWebhookService>();

services.AddScoped<IWebhookService, ClickUpWebhookService>();

services.AddTransient<GetWebhooksUseCase>();
services.AddTransient<CreateWebhookUseCase>();
services.AddTransient<UpdateWebhookUseCase>();
services.AddTransient<DeleteWebhookUseCase>();

services.AddTransient<ISubCommandHandler, CreateWebhookSubCommand>();
services.AddTransient<ISubCommandHandler, ListWebhooksSubCommand>();
services.AddTransient<ISubCommandHandler, UpdateWebhookSubCommand>();
services.AddTransient<ISubCommandHandler, DeleteWebhookSubCommand>();

services.AddTransient<ICommandHandler, WebhookCommand>();
services.AddTransient<ICommandHandler, VersionCommand>();
services.AddTransient<ICommandHandler, ConfigCommand>();

services.AddTransient<KodoRootCommand>();

services.AddTransient<App>();

var serviceProvider = services.BuildServiceProvider();

var app = serviceProvider.GetRequiredService<App>();
return await app.RunAsync(args);