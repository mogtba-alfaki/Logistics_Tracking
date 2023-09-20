using RabbitMQ.Client;

namespace Infrastructure.MessageBroker; 

public class RabbitMqConnection {
    private string Host = Environment.GetEnvironmentVariable("RABBITMQ_HOST"); 
    public int Port = Convert.ToInt32(Environment.GetEnvironmentVariable("RABBITMQ_PORT")); 
    private string Password =   Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD"); 
    private  string QueueName =  Environment.GetEnvironmentVariable("MAIN_QUEUE_NAME");
    private ConnectionFactory _factory; 
    public IModel GetChannel () {
        _factory = new ConnectionFactory {
            Uri = new Uri($"amqp://guest:guest@{Host}:{Port}/"),
            Password = Password,
        }; 
        
        var connection = _factory.CreateConnection();
        IModel channel;
        channel = connection.CreateModel();
            channel.QueueDeclare(QueueName); 
        return channel;
    }
}