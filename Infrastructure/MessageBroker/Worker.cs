
namespace Infrastructure.MessageBroker; 

public class Worker {
    private readonly Consumer _consumer = new Consumer(); 
    private  readonly  string QueueName =  Environment.GetEnvironmentVariable("MAIN_QUEUE_NAME");

    public void Run() {
        Console.WriteLine("Worker Running .....");
         _consumer.Consume(QueueName);
        Thread.Sleep(Timeout.Infinite);
    } 
}