using Application.Interfaces;
using Application.UseCases.CreateWebhook;
using Application.UseCases.DeleteWebhook;
using Application.UseCases.GetWebhooks;
using Application.UseCases.UpdateWebhook;
using Infrastructure.ClickUp.Services;
using Infrastructure.Config;
using Kodo;
using Kodo.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
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

services.AddTransient<ICommandHandler, WebhookCommand>();

/*
 services.AddTransient<ICommandHandler>(sp =>
{
    var subCommands = new ICommandHandler[]
    {
        sp.GetRequiredService<CreateWebhookCommand>(),
        sp.GetRequiredService<ListWebhookCommand>()
    };
    return new WebhookCommand(subCommands);
});
 */

services.AddTransient<KodoRootCommand>();

services.AddTransient<App>();

var serviceProvider = services.BuildServiceProvider();

var app = serviceProvider.GetRequiredService<App>();
return await app.RunAsync(args);