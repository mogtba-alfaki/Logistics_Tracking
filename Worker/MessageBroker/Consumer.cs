using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Worker.MessageBroker; 

public class Consumer {
    private RabbitMqConnection _connection; 

    
    public void Subscribe(RabbitMqConnection connection, string queueName, Func<string, Task> ProcessLocation) {
        var channel = connection.GetChannel(); 
        var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (sender, e) => {
                var messageBytes = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(messageBytes);
                Console.WriteLine($"A Message Received, Message: {message}"); 
                await Task.Run(() => ProcessLocation(message));
            };
            channel.BasicConsume(queueName, true, consumer); 
    }
}