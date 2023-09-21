using System.Text;
using Core.Interfaces.MessageQueue;

namespace Worker.MessageBroker;

public class Publisher : IPublisher {
     private string ExchangeName = Environment.GetEnvironmentVariable("MAIN_EXCHANGE_NAME");
     private RabbitMqConnection connection = new();  
     public  async Task PublishMessage(string message) {
         var channel = connection.GetChannel(); 
            byte[] bytes = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(ExchangeName,
                    "", false, null,bytes);
    }
}