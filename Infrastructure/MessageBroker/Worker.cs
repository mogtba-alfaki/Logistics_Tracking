using System.Text.Json;
using Core.Helpers;
using Core.Trips.Dto;
using Core.Trips.UseCases;
using Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
namespace Infrastructure.MessageBroker; 

public class Worker {
    private readonly Consumer _consumer;  
    private  readonly  string QueueName =  Environment.GetEnvironmentVariable("MAIN_QUEUE_NAME");
    private  readonly  string ExchangeName =  Environment.GetEnvironmentVariable("MAIN_EXCHANGE_NAME");
    private readonly RabbitMqConnection _connection;
    private readonly IModel _channel;

    public Worker() {
        _connection =  new RabbitMqConnection(); 
        _channel = _connection.GetChannel();
        _consumer = new Consumer();
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

    private async Task ProcessLocation(string LocationMessage) {
        var dto = JsonSerializer.Deserialize<TripLocationDto>(LocationMessage);
        // process 
    }   
}