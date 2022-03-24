using FastEndpoints;
using FastEndpoints.Swagger;
using IotProto.Host.Extensions;
using IotProto.Host.Integrations.MQTT.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddActorSystem();
builder.Services.AddMqttIntegration(builder.Configuration);
builder.Services.AddFastEndpoints();
builder.Services.AddSwaggerDoc();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseAuthorization();
app.UseFastEndpoints();
app.UseOpenApi();
app.UseSwaggerUi3(s => s.ConfigureDefaults());
app.UseHealthChecks("/health");

app.Run();