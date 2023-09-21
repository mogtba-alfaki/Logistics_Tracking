namespace Core.Interfaces.MessageQueue; 

public interface IPublisher {
    public Task PublishMessage(string message); 
}
