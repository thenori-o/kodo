using Application.Interfaces.Webhook;
using Application.UseCases.Webhook.CreateWebhook;
using Application.UseCases.Webhook.DeleteWebhook;
using Application.UseCases.Webhook.GetWebhooks;
using Application.UseCases.Webhook.UpdateWebhook;
using Infrastructure.ClickUp.Services;
using Infrastructure.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<KodoSettings>(builder.Configuration.GetSection("KodoSettings"));
builder.Services.AddHttpClient<ClickUpWebhookService>();

builder.Services.AddScoped<IWebhookService, ClickUpWebhookService>();

builder.Services.AddScoped<GetWebhooksUseCase>();
builder.Services.AddScoped<CreateWebhookUseCase>();
builder.Services.AddScoped<UpdateWebhookUseCase>();
builder.Services.AddScoped<DeleteWebhookUseCase>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
