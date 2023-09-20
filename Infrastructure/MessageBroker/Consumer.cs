using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Infrastructure.MessageBroker; 

public class Consumer {
    private RabbitMqConnection _connection; 
    private IModel _channel; 
    private string ExchangeName = Environment.GetEnvironmentVariable("MAIN_EXCHANGE_NAME");

    public Consumer() {
        _connection = new RabbitMqConnection();
        _connection.GetChannel(); 
    }
    

    public string Consume(string queueName) {
        var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (sender, e) => {
                var messageBytes = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(messageBytes);
                Console.WriteLine($"A Message Received, Message: {message}");
                // process Message here .
            };
            _channel.BasicConsume(queueName, true, consumer); 
         return "Got A message"; 
    }
}