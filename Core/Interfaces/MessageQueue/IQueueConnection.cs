namespace Core.Interfaces.MessageQueue; 

public interface IQueueConnection {
    public object GetChannel(); 
}