using System;
using MQTTnet;
using MQTTnet.Server;
using System.Threading.Tasks;
using System.Text;

namespace brokerMQ
{
    public class Program
    {
         static async Task Main(string[] args)
        {

            var optionsBuilder = new MqttServerOptionsBuilder()
            .WithConnectionBacklog(100)
            .WithDefaultEndpointPort(1884);

            var mqttServer = new MqttFactory().CreateMqttServer();
            await mqttServer.StartAsync(optionsBuilder.Build());

            mqttServer.ApplicationMessageReceived += (s, e) =>
            {
                Console.WriteLine();
                Console.WriteLine("### Mensagem Recebida ###");
                Console.WriteLine($"+ ClientId = {e.ClientId}");
                Console.WriteLine($"+ Topico = {e.ApplicationMessage.Topic}");
                Console.WriteLine($"+ Mensagem = {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");
                Console.WriteLine($"+ QoS = {e.ApplicationMessage.QualityOfServiceLevel}");
                Console.WriteLine($"+ Retain = {e.ApplicationMessage.Retain}");
                Console.WriteLine(); 
            };

            Console.WriteLine("Server Mqtt iniciado...");
            Console.WriteLine("Pressione qualquer tecla para fechar");
            Console.ReadLine();
            await mqttServer.StopAsync();
        }
    }
}
