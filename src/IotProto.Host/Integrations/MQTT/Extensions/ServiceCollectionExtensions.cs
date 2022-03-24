using IotProto.Host.Integrations.MQTT.Configuration;

namespace IotProto.Host.Integrations.MQTT.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddMqttIntegration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MqttConfiguration>(configuration.GetSection("integrations:mqtt"));
        services.AddHostedService<MqttHostedService>();
    }
}