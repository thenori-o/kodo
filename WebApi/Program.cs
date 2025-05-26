using Application.Interfaces;
using Infrastructure.ClickUp.Services;
using Infrastructure.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<Settings>(builder.Configuration.GetSection("ClickUp"));
builder.Services.AddHttpClient<ClickUpWebhookService>();

builder.Services.AddScoped<IWebhookService, ClickUpWebhookService>();

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
