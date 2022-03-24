using System.Security.Authentication;
using IotProto.Host.Integrations.MQTT.Configuration;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;

namespace IotProto.Host.Integrations.MQTT;

public class MqttHostedService : IHostedService
{
    private readonly ILogger<MqttHostedService> _logger;
    private readonly IOptions<MqttConfiguration> _options;

    private IMqttClient _mqttClient;

    public MqttHostedService(ILogger<MqttHostedService> logger, IOptions<MqttConfiguration> options)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Mqtt Integration started");
        
        _mqttClient = new MqttFactory().CreateMqttClient();
        var mqttClientOptions = new MqttClientOptionsBuilder()
            .WithClientId("iotproto")
            .WithCleanSession()
            .WithTcpServer(_options.Value.Url, _options.Value.Port)
            .Build();

        _mqttClient.UseConnectedHandler(OnConnected);
        _mqttClient.UseDisconnectedHandler(OnDisconnected);
        await _mqttClient.ConnectAsync(mqttClientOptions, cancellationToken);
    }

    private Task OnConnected(MqttClientConnectedEventArgs arg)
    {
        _logger.LogInformation("Mqtt Integration connected");
        return Task.CompletedTask;
    }

    private Task OnDisconnected(MqttClientDisconnectedEventArgs arg)
    {
        _logger.LogInformation("Mqtt Integration disconnected");
        return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _mqttClient.DisconnectAsync(cancellationToken);
        _logger.LogInformation("Mqtt Integration stopped");
    }
}