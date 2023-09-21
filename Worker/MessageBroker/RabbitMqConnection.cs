using RabbitMQ.Client;

namespace Worker.MessageBroker;

public class RabbitMqConnection {
    private string Host = Environment.GetEnvironmentVariable("RABBITMQ_HOST"); 
    public int Port = Convert.ToInt32(Environment.GetEnvironmentVariable("RABBITMQ_PORT")); 
    private string Password =   Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD"); 
    private  string QueueName =  Environment.GetEnvironmentVariable("MAIN_QUEUE_NAME");
    private ConnectionFactory _factory;
    private IConnection _connection;
    private readonly IModel _channel; 
    public RabbitMqConnection() {
        _factory = new ConnectionFactory {
            Uri = new Uri($"amqp://guest:guest@{Host}:{Port}/"),
        };
        _connection = _factory.CreateConnection();
        _channel = _connection.CreateModel(); 
    }

    public IModel GetChannel () {
        return _channel;
    }    
}