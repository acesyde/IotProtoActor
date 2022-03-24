namespace IotProto.Host.Integrations.MQTT.Configuration;

public class MqttConfiguration
{
    public string Url { get; set; }
    public int Port { get; set; } = 1883;
}