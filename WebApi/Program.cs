using ClickUpSdk;
using ClickUpSdk.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<ClickUpSettings>(builder.Configuration.GetSection("ClickUp"));
builder.Services.AddHttpClient<ClickUpClient>();

builder.Services.AddScoped<ClickUpClient>();

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
