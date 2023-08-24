namespace Core.Interfaces; 

public interface ILogger {
    public void LogInfo(string message);
    public void LogDebug(string message);
    public void LogError(string message);
    public void LogWarn(string message); 
}