using Core.Interfaces.MessageQueue;

namespace Core.Interfaces; 

public interface IPublisher {
    public Task PublishMessage(string message); 
}
