using System.Text;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;

namespace Infrastructure.MessageBroker; 

public class Publisher {
     RabbitMqConnection _connection = new RabbitMqConnection();
     private readonly IModel _channel; 
     private string ExchangeName = Environment.GetEnvironmentVariable("MAIN_EXCHANGE_NAME");

     public Publisher() {
         _channel = _connection.GetChannel(); 
     }

     public  void PublishMessage(string message) {
            byte[] bytes = Encoding.UTF8.GetBytes(message); 
            _channel
                .BasicPublish(ExchangeName, "", false, null,bytes);
    }
}