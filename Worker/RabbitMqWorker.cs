using System.Text.Json;
using Core.Trips.Dto;
using Domain.Entities;
using Infrastructure.Database;
using Worker.MessageBroker;
using RabbitMQ.Client;
using Worker.Services;

namespace Worker; 

public class RabbitMqWorker {
    private readonly Consumer _consumer;  
    private  readonly  string QueueName =  Environment.GetEnvironmentVariable("MAIN_QUEUE_NAME");
    private  readonly  string ExchangeName =  Environment.GetEnvironmentVariable("MAIN_EXCHANGE_NAME");
    private readonly RabbitMqConnection _connection;
    private readonly IModel _channel;
    private readonly TrackingContext _context;
    private readonly LocationServices _locationServices;
    public RabbitMqWorker(TrackingContext context, LocationServices locationServices) {
        _connection =  new RabbitMqConnection(); 
        _channel = _connection.GetChannel();
        _consumer = new Consumer();
        _context = context;
        _locationServices = locationServices;
    }

    public void Run() {
        _channel.ExchangeDeclare(ExchangeName, ExchangeType.Direct, 
            true, true);
        _channel.QueueDeclare(QueueName);
        _channel.QueueBind(QueueName, ExchangeName, "");
        Console.WriteLine("Worker Running .....");
        _consumer.Subscribe(_connection, QueueName, ProcessLocation);
        Thread.Sleep(Timeout.Infinite);
    }
    private async Task ProcessLocation(string locationMessage) {
        var dto = JsonSerializer.Deserialize<TripLocationDto>(locationMessage);
        await _locationServices.UpdateLocation(dto);
    }   
}